using SendGridAzureEmail.Models;
using System;
using System.Net;
using System.Web.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGridAzureEmail.Class;

namespace SendGridAzureEmail.Controllers
{
    public class EmailSGController : ApiController
    {
        SecurityClass security = new SecurityClass();
        ResponseModel response = new ResponseModel();

        public async Task<int> SendEmail2([FromBody] EmailModel parametros)
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
            mail.Body = parametros.Messagge; //"<h2 style=\"color:red;\">" + "HOLA" + "</h2>";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = security.DesEncriptar("cwBtAHQAcAAuAG8AZgBmAGkAYwBlADMANgA1AC4AYwBvAG0A");
            string port=security.DesEncriptar("NQA4ADcA");
            smtpClient.Port =Convert.ToInt32(port);
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
