using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendGridAzureEmail.Models
{
    public class SendGridModel
    {
        //public string Messagge { get; set; }
        public List<EmailAddress> EmailsAddressTO { get; set; }
        public string EmailsAddressFROM { get; set; }
        public string Subject { get; set; }
        public bool displayRecipients { get; set; }
        public string HTML { get; set; }
        public string TEXT { get; set; }
        public string key { get; set; }
    }
}