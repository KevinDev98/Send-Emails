using SendGridAzureEmail.Models;
using SendGridAzureEmail.Class;
using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using SendGrid.Helpers.Mail;
using System.Net.Mime;
using System.Web.Http;
using System.Net.Mail;
using EllipticCurve.Utils;
using SendGrid;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace SendGridAzureEmail.Controllers
{
    public class EmailSGController : ApiController
    {
        SecurityClass security = new SecurityClass();
        ResponseModel response = new ResponseModel();
        AzureClass azure = new AzureClass();
        SendGridEMailClass sg= new SendGridEMailClass();
        string v_Html0, v_Html1, v_Html2, v_Html3;
        string sendfile;
        int result, resultsend;
        public int SendNotifyEMail([FromBody] SendGridModel parametros)
        {
            sg.EMailNotify(parametros.key,parametros.EmailsAddressFROM,parametros.EmailsAddressTO,parametros.Subject).Wait();
            return resultsend;  
        }
        Stream streamAzure;
        //string ky;
        //public async Task<int> PostEMailSG([FromBody] SendGridModel parametros)
        //{
        //    ky = parametros.key;//security.DesEncriptar(parametros.key);
        //    SendGridClient client = new SendGridClient(ky);

        //    //var from2 = new EmailAddress("test1@example.com", "Example User 1");
        //    //EmailAddress(«test2@example.com», «Example User 2»)
        //    var from = new EmailAddress(parametros.EmailsAddressFROM);
        //    List<EmailAddress> Para = parametros.EmailsAddressTO;
        //    var subject = parametros.Subject;

        //    var htmlContent = "< strong > Hello world with HTML content</ strong >";
        //    var plaintextContent = "Hello world with TEXT content";
        //    bool displayRecipients = false; // set this to true if you want recipients to see each others mail id

        //    //var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, Para, subject, plaintextContent, htmlContent, displayRecipients);
        //    var msg = MailHelper.CreateSingleEmail(from, Para[0], subject, plaintextContent, htmlContent);
        //    var response = client.SendEmailAsync(msg);
        //    return result;

        //}
    }
}
