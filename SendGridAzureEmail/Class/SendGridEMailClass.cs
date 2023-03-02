using SendGrid.Helpers.Mail;
using SendGrid;
using SendGridAzureEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Http;

namespace SendGridAzureEmail.Class
{
    public class SendGridEMailClass
    {
        string ky;
        int result = 0;
        public async Task EMailNotify(string key, String FromEM, List<EmailAddress> TOEM, string Subject)
        {
            try
            {
                ky = key; //parametros.key;//security.DesEncriptar(parametros.key);
                SendGridClient client = new SendGridClient(ky);

                //var from2 = new EmailAddress("test1@example.com", "Example User 1");
                //EmailAddress(«test2@example.com», «Example User 2»)
                var from = new EmailAddress(FromEM);
                List<EmailAddress> Para = TOEM;
                var subject = Subject;

                var htmlContent = "< strong > Hello world with HTML content</ strong >";
                var plaintextContent = "Hello world with TEXT content";
                bool displayRecipients = false; // set this to true if you want recipients to see each others mail id

                //var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, Para, subject, plaintextContent, htmlContent, displayRecipients);
                var msg = MailHelper.CreateSingleEmail(from, Para[0], subject, plaintextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}