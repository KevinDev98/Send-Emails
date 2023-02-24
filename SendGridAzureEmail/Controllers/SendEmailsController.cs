using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
//using System.Web.Mvc.Routing;
using EllipticCurve.Utils;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGridAzureEmail.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SendGridAzureEmail.Controllers
{
    //[Route("api/SendEmails")]
    public class SendEmailsController : ApiController
    {
        [HttpPost]
        [ActionName("SendProcCompletNoti")]
        public async Task SendProcCompletNoti([FromBody] EmailModel parametros)

        {
            string keySG = "DtoTSqG3OJ18gPXAw.aQLsI1bCkitm4pQnob6Gv4f7ehlx2H3lmDR5vrouroc";
            SendGridClient client = new SendGridClient(keySG);
            //var from2 = new EmailAddress("test1@example.com", "Example User 1");
            //EmailAddress(«test2@example.com», «Example User 2»)
            var from = new EmailAddress(parametros.EmailsAddressFROM);
            List<EmailAddress> Para = parametros.EmailsAddressTO;
            var subject = parametros.Subject;

            var htmlContent = "< strong > Hello world with HTML content</ strong >";
            var plaintextContent = "Hello world with TEXT content";
            bool displayRecipients = false; // set this to true if you want recipients to see each others mail id

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, Para, subject, plaintextContent, htmlContent, displayRecipients);
            var response = await client.SendEmailAsync(msg);
        }

        [HttpPost]
        [ActionName("SendEmail")]
        public IActionResult SendEmail([FromBody] EmailModel parametros)
        {
            string keySG = "DtoTSqG3OJ18gPXAw.aQLsI1bCkitm4pQnob6Gv4f7ehlx2H3lmDR5vrouroc";
            SendGridClient client = new SendGridClient(keySG);

            //var from2 = new EmailAddress("test1@example.com", "Example User 1");
            //EmailAddress(«test2@example.com», «Example User 2»)
            var from = new EmailAddress(parametros.EmailsAddressFROM);
            List<EmailAddress> Para = parametros.EmailsAddressTO;
            var subject = parametros.Subject;

            var htmlContent = "< strong > Hello world with HTML content</ strong >";
            var plaintextContent = "Hello world with TEXT content";
            bool displayRecipients = false; // set this to true if you want recipients to see each others mail id

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, Para, subject, plaintextContent, htmlContent, displayRecipients);
            var response = client.SendEmailAsync(msg);
            return (IActionResult)response;
        }
    }
}
    