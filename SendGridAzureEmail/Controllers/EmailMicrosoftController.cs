using System;
using System.Collections.Generic;
using System.Linq;
using SendGridAzureEmail.Models;
using SendGridAzureEmail.Class;
using System.Net.Mail;
using System.Web.Http;
using System.Net;
using System.Net.Mime;
using System.IO;
using System.Configuration;
using System.Web;
using System.Web.Http.Results;
using System.Net.Http;
using DDLEncrypt;

namespace SendGridAzureEmail.Controllers
{
	public class EmailMicrosoftController : ApiController
	{
		//SecurityClass security = new SecurityClass();
		Encrypt security = new Encrypt();
		ResponseModel response = new ResponseModel();
		AzureClass azure = new AzureClass();
		MailMessage mail = new MailMessage();
		SmtpClient smtpClient = new SmtpClient();
		String usermail, passwordmail;
		string v_Html0, v_Html1, v_Html2, v_Html3;
		string sendfile;
		string port;
		Attachment data;
		Stream streamAzure;
		HttpResponseMessage msg = new HttpResponseMessage();
		string str_response;
		public HttpResponseMessage SendEmailContainer([FromBody] EmailModel parametros)
		{
			if (string.IsNullOrEmpty(parametros.Subject) || string.IsNullOrEmpty(parametros.Messagge) || string.IsNullOrEmpty(parametros.Container) || parametros.EmailsAddressTO.Count == 0)
			{
				str_response = "Parametros vacios";
				return Request.CreateResponse(HttpStatusCode.Unauthorized, new { response = str_response });
			}
			else
			{
				try
				{
					//Obtención de archivos
					string urlcont = azure.GetUrl(parametros.Container);
					List<string> ContainersFiles = azure.ListBlobFile(PathBlob: urlcont, ContainerBlobName: parametros.Container);
					//Inicialización de remitente
					usermail = security.DesEncriptar(ConfigurationManager.AppSettings["MEMail"]);
					passwordmail = security.Fulldesencrypt(ConfigurationManager.AppSettings["Mpwd"]).ToLower();
					mail.From = new MailAddress(usermail);
					// Añadiendo destinatarios
					for (int i = 0; i < parametros.EmailsAddressTO.Count; i++)
					{
						mail.To.Add(parametros.EmailsAddressTO[i]);
					}
					try
					{
						for (int i = 0; i < parametros.EmailsAddressCC.Count; i++)
						{
							mail.CC.Add(parametros.EmailsAddressCC[i]);
						}
					}
					catch (Exception ex)
					{

					}
					mail.Subject = parametros.Subject.ToString();//Titulo del correo            
																 //Definiendo estructura del mensaje
					v_Html0 = "<p>" + parametros.Messagge + "</p>" + "<br>";
					v_Html1 = v_Html0 + "<table class='table align-content-start table-bordered shadow'> " + "<tr>" + "<td colspan=\"2\" style=\"background-color: #3366CC; color: #FFFFFF; font-weight: bold; text-align: center;\">Found Files into container " + parametros.Container + "</td> " + "</tr> ";
					if (ContainersFiles.Count > 0)
					{
						v_Html2 = " <tr> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">File Name</td>" + " <td style=\"background-color: #E8E8EC; font-weight: bold\">Path File</td> " + " </tr> ";
						for (int z = 0; z < ContainersFiles.Count; z++)
						{
							v_Html2 = v_Html2 + " <tr> " + " <td>" + ContainersFiles[z] + "</td> " + " <td>" + urlcont + ContainersFiles[z] + "</td>" + " </tr> ";
						}
						v_Html3 = " </table>";
						parametros.Messagge = v_Html1 + v_Html2 + v_Html3;
						mail.IsBodyHtml = true;
					}
					else
					{
						mail.Body = parametros.Messagge; //"<h2 style=\"color:red;\">" + "HOLA" + "</h2>";
						mail.IsBodyHtml = false;
					}
					//Añaiendo el mensaje
					var htmlView = AlternateView.CreateAlternateViewFromString(parametros.Messagge, null, "text/html");
					mail.AlternateViews.Add(htmlView);

					//Configuración de envio
					mail.Priority = MailPriority.Normal;
					smtpClient.Host = security.DesEncriptar(ConfigurationManager.AppSettings["MHost"]);
					port = security.DesEncriptar(ConfigurationManager.AppSettings["Mport"]);
					smtpClient.Port = Convert.ToInt32(port);
					smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
					smtpClient.EnableSsl = true;

					smtpClient.UseDefaultCredentials = false;
					NetworkCredential credential = new NetworkCredential(usermail, passwordmail);
					smtpClient.Credentials = credential;

					try
					{
						//await smtpClient.SendMailAsync(mail);//Envio del correo
						smtpClient.Send(mail);//Envio del correo
						str_response = "The email sent successfully";
						return Request.CreateResponse(HttpStatusCode.OK, new { response = str_response });
					}
					catch (Exception ex)
					{
						str_response = "The email was not send " + ex.Message + " " + ex.InnerException;
						return Request.CreateResponse(HttpStatusCode.BadRequest, new { response = str_response });
					}
				}
				catch (Exception ex)
				{
					str_response = "The email was not send " + ex.Message + " " + ex.InnerException;
					return Request.CreateResponse(HttpStatusCode.BadRequest, new { response = str_response });
				}

			}

		}
		public HttpResponseMessage SendNotify([FromBody] EmailModel parametros)
		{
			if (string.IsNullOrEmpty(parametros.Subject) || string.IsNullOrEmpty(parametros.Messagge) || parametros.EmailsAddressTO.Count == 0 || parametros.PriorityHigh == null)
			{
				str_response = "Parametros vacios";
				return Request.CreateResponse(HttpStatusCode.Unauthorized, new { response = str_response });
			}
			try
			{
				//definiendo remitente
				usermail = security.DesEncriptar(ConfigurationManager.AppSettings["MEMail"]);
				passwordmail = security.Fulldesencrypt(ConfigurationManager.AppSettings["Mpwd"]).ToLower();
				mail.From = new MailAddress(usermail);
				//añdiendo destinatarios
				for (int i = 0; i < parametros.EmailsAddressTO.Count; i++)
				{
					mail.To.Add(parametros.EmailsAddressTO[i]);
				}
				try
				{
					for (int i = 0; i < parametros.EmailsAddressCC.Count; i++)
					{
						mail.CC.Add(parametros.EmailsAddressCC[i]);
					}
				}
				catch (Exception)
				{

				}

				mail.Subject = parametros.Subject.ToString();
				//Definiendo mensaje y estructura
				if (!parametros.PriorityHigh)
				{
					mail.Body = "<h3 style=\"color:green;\">" + parametros.Messagge + "</h3>";
					mail.Priority = MailPriority.Normal;
				}
				else
				{
					mail.Body = "<h3 style=\"color:red;\">" + parametros.Messagge + "</h3>";
					mail.Priority = MailPriority.High;
				}
				mail.IsBodyHtml = true;

				//Configuración de envio            
				smtpClient.Host = security.DesEncriptar(ConfigurationManager.AppSettings["MHost"]);
				port = security.DesEncriptar(ConfigurationManager.AppSettings["Mport"]);
				smtpClient.Port = Convert.ToInt32(port);
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

				smtpClient.EnableSsl = true;
				smtpClient.UseDefaultCredentials = false;
				NetworkCredential credential = new NetworkCredential(usermail, passwordmail);
				smtpClient.Credentials = credential;

				try
				{
					//await smtpClient.SendMailAsync(mail);//Envio del correo
					smtpClient.Send(mail);//Envio del correo
					str_response = "The email sent successfully";
					return Request.CreateResponse(HttpStatusCode.OK, new { response = str_response });
				}
				catch (Exception ex)
				{
					str_response = "The email was not send " + ex.Message + " " + ex.InnerException;
					return Request.CreateResponse(HttpStatusCode.BadRequest, new { response = str_response }); ;
				}
			}
			catch (Exception ex)
			{
				str_response = "The email was not send " + ex.Message + " " + ex.InnerException;
				return Request.CreateResponse(HttpStatusCode.BadRequest, new { response = str_response });
			}
		}
		public HttpResponseMessage SendDocumentsReports([FromBody] EmailModel parametros)
		{
			if (string.IsNullOrEmpty(parametros.Subject) || string.IsNullOrEmpty(parametros.Messagge) || string.IsNullOrEmpty(parametros.Container) || parametros.EmailsAddressTO.Count == 0 || parametros.NombresArchivos.Count == 0)
			{
				str_response = "Parametros vacios";
				return Request.CreateResponse(HttpStatusCode.Unauthorized, new { response = str_response });
			}
			try
			{
				////Obtención de  que se encuentran en azure
				string urlcont = azure.GetUrl(parametros.Container);
				List<string> ContainersFiles = azure.ListBlobFile(PathBlob: urlcont, ContainerBlobName: parametros.Container);
				//definiendo remitente
				usermail = security.DesEncriptar(ConfigurationManager.AppSettings["MEMail"]);
				passwordmail = security.Fulldesencrypt(ConfigurationManager.AppSettings["Mpwd"]).ToLower();
				mail.From = new MailAddress(usermail);
				//añdiendo destinatarios
				for (int i = 0; i < parametros.EmailsAddressTO.Count; i++)
				{
					mail.To.Add(parametros.EmailsAddressTO[i]);
				}
				try
				{
					for (int i = 0; i < parametros.EmailsAddressCC.Count; i++)
					{
						mail.CC.Add(parametros.EmailsAddressCC[i]);
					}
				}
				catch (Exception)
				{

				}
				mail.Subject = parametros.Subject.ToString();
				//Definiendo mensaje y estructura
				mail.Body = "<h4>" + parametros.Messagge + "</h4>";
				mail.IsBodyHtml = true;
				
				int detected_files = 0;
				string filesforsend = "";

				while (detected_files != parametros.NombresArchivos.Count())
				{
					int z = 0;
					for (int s = 0; s < ContainersFiles.Count; s++)
					{
						sendfile = urlcont + ContainersFiles[s];
						string FName = ContainersFiles[s].ToString();
						try
						{
							try
							{
								filesforsend = parametros.NombresArchivos[z].ToString();
								z = s;
							}
							catch (Exception)
							{
								z = s - 1;
								filesforsend = parametros.NombresArchivos[z].ToString();
							}
							if (FName.Contains(filesforsend))
							{
								streamAzure = azure.StreamGetStream(parametros.Container, FName);
								// Añadiendo archivos.
								data = new Attachment(streamAzure, FName, MediaTypeNames.Application.Octet);
								mail.Attachments.Add(data);
								detected_files = detected_files + 1;
								if (detected_files == parametros.NombresArchivos.Count())
								{
									break;
								}
							}
						}
						catch (Exception ex)
						{

						}
					}
				}
				//Configuración de envio
				mail.Priority = MailPriority.High;
				smtpClient.Host = security.DesEncriptar(ConfigurationManager.AppSettings["MHost"]);
				port = security.DesEncriptar(ConfigurationManager.AppSettings["Mport"]);
				smtpClient.Port = Convert.ToInt32(port);
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.EnableSsl = true;

				smtpClient.UseDefaultCredentials = false;
				NetworkCredential credential = new NetworkCredential(usermail, passwordmail);
				smtpClient.Credentials = credential;

				try
				{
					//await smtpClient.SendMailAsync(mail);//Envio del correo
					smtpClient.Send(mail);//Envio del correo
					str_response = "The email sent successfully";
					return Request.CreateResponse(HttpStatusCode.OK, new { response = str_response });
				}
				catch (Exception ex)
				{
					str_response = "The email was not send " + ex.Message + " " + ex.InnerException;
					return Request.CreateResponse(HttpStatusCode.BadRequest, new { response = str_response });
				}
			}
			catch (Exception ex)
			{
				str_response = "The email was not send " + ex.Message + " " + ex.InnerException;
				return Request.CreateResponse(HttpStatusCode.BadRequest, new { response = str_response });
			}
		}
	}
}