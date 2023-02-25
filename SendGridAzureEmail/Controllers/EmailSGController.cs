using SendGridAzureEmail.Models;
using System;
using System.Net;
using System.Web.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGridAzureEmail.Class;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace SendGridAzureEmail.Controllers
{
    public class EmailSGController : ApiController
    {
        SecurityClass security = new SecurityClass();
        ResponseModel response = new ResponseModel();
        AzureClass azure = new AzureClass();
        public void ReporteAsignacion_Mail(ref string Subject, string Titulo, object V_Msg, int v_bandera, string Horaejecucion)
        {
            //string v_mensaje = "";
            //if (v_bandera == 1)
            //{
            //    string v_Html1 = "";
            //    string v_Html2 = "";
            //    string v_Html3 = "";
            //    v_Html1 = "<table class='table align-content-start table-bordered shadow'> " + "<tr>" + "<td colspan=\"12\" style=\"background-color: #3366CC; color: #FFFFFF; font-weight: bold; text-align: center;\">" + Subject + "" + Horaejecucion + "</td> " + "</tr> ";
            //    if (V_Msg.Tables(0).Rows.Count > 0)
            //    {
            //        v_Html2 = " <tr> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">NOMBRE</td>" + " <td style=\"background-color: #E8E8EC; font-weight: bold\">PUESTO</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">BUCKETS ASIGNADOS</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">SUCURSALES ASIGNADAS</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">CAPACIDAD ASIGNACIÓN WEB</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">CAPACIDAD ASIGNACIÓN MOVIL</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">TOTAL ASIGNADAS</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">ASIGNACIÓN WEB POR REPARTO DE CUENTAS</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">OTRAS ASIGNACIONES WEB</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">ASIGNACIÓN MOVIL POR ORIGINACIÓN DE CREDITO</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">ASIGNACIÓN MOVIL POR REPARTO DE CUENTAS</td> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">OTRAS ASIGNACIONES MOVIL </td> " + " </tr> ";
            //        for (int a = 0; a <= V_Msg.Tables(0).Rows.Count - 1; a++)
            //            v_Html2 = v_Html2 + " <tr> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("NOMBRE") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("PUESTO") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("BUCKETS ASIGNADOS") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("SUCURSALES ASIGNADAS") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("CAPACIDAD ASIGNACIÓN WEB") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("CAPACIDAD ASIGNACIÓN MOVIL") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("TOTAL ASIGNADAS") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("ASIGNACIÓN WEB POR REPARTO DE CUENTAS") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("OTRAS ASIGNACIONES WEB") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("ASIGNACIÓN MOVIL POR ORIGINACIÓN DE CREDITO") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("ASIGNACIÓN MOVIL POR REPARTO DE CUENTAS") + "</td> " + " <td>" + V_Msg.Tables(0).Rows(a).Item("OTRAS ASIGNACIONES MOVIL") + "</td> " + " </tr> ";
            //        v_Html3 = " </table>";
            //        v_mensaje = v_Html1 + v_Html2 + v_Html3;
            //    }
            //    else
            //    {
            //        v_mensaje = V_Msg.ToString();
            //    }
            //}
            //System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //msg.From = new MailAddress("mc.notificaciones@alternativa19delsur.com", Titulo, System.Text.Encoding.UTF8);
            //msg.Subject = Subject;
            //msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //msg.AlternateViews.Add(htmlView);
            //msg.BodyEncoding = System.Text.Encoding.UTF8;
            //msg.IsBodyHtml = true;

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //SmtpClient client = new SmtpClient();
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential("mc.notificaciones@alternativa19delsur.com", "43q6kKnNU*");
            //// client.Port = "587"
            //client.Host = "mail.centraldomainnames.com";
            //client.EnableSsl = true;
            //try
            //{
            //    client.Send(msg);
            //}
            //catch (Net.Mail.SmtpException ex)
            //{
            //    SP.AUDITORIA_GLOBAL(0, tmpUSUARIO("CAT_LO_USUARIO"), "Administrador-Cartera general", "Fallo al enviar reporte de asignación" + ex.Message.ToString());
            //}
        }
        public async Task<int> SendEmailContainer([FromBody] EmailModel parametros)
        {
            MailMessage mail = new MailMessage();
            String usermail = parametros.EmailsAddressFROM;
            String passwordmail = security.DesEncriptar("SwBlAHYAaQBuAGkAbgBnAGkAbgBmAA==");
            mail.From = new MailAddress(usermail);

            for (int i = 0; i < parametros.EmailsAddressTO.Count; i++)
            {
                mail.To.Add(parametros.EmailsAddressTO[i]);
            }
            mail.Subject = parametros.Subject.ToString();
            string urlcont = azure.GetUrlContainer(parametros.Container);
            List<string> ContainersFiles = azure.ListBlobFile(PathBlob: urlcont, ContainerBlobName: parametros.Container);

            string v_Html1 = "";
            string v_Html2 = "";
            string v_Html3 = "";            
            
            v_Html1 = "<table class='table align-content-start table-bordered shadow'> " + "<tr>" + "<td colspan=\"2\" style=\"background-color: #3366CC; color: #FFFFFF; font-weight: bold; text-align: center;\">Archivos procesados en el contenedor "+parametros.Container +"</td> " + "</tr> ";
            if (ContainersFiles.Count> 0)
            {
                v_Html2 = " <tr> " + " <td style=\"background-color: #E8E8EC; font-weight: bold\">NOMBRE</td>" + " <td style=\"background-color: #E8E8EC; font-weight: bold\">URL</td> " + " </tr> ";
                for (int z=0; z<ContainersFiles.Count;z++)
                {
                    v_Html2 = v_Html2 + " <tr> " + " <td>" + ContainersFiles[z] + "</td> " + " <td>" + urlcont+ContainersFiles[z] + "</td>" + " </tr> ";
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
            var htmlView = AlternateView.CreateAlternateViewFromString(parametros.Messagge, null, "text/html");
            mail.AlternateViews.Add(htmlView);
            mail.Priority = MailPriority.Normal;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = security.DesEncriptar("cwBtAHQAcAAuAG8AZgBmAGkAYwBlADMANgA1AC4AYwBvAG0A");
            string port = security.DesEncriptar("NQA4ADcA");
            smtpClient.Port = Convert.ToInt32(port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            NetworkCredential credential =
                new NetworkCredential(usermail, passwordmail);
            smtpClient.Credentials = credential;
            try
            {
                await smtpClient.SendMailAsync(mail);
                response.CodeResponse = 1;
                response.MessageResponse = "Email enviado correctamente";
            }
            catch (Exception ex)
            {
                response.CodeResponse = 0;
                response.MessageResponse = "Email NO Enviado";
            }
            return response.CodeResponse;
        }
    }
}
