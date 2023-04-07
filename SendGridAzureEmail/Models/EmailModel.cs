using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendGridAzureEmail.Models
{
    public class EmailModel
    {        
        public List<String> EmailsAddressTO { get; set; }
        public List<String> EmailsAddressCC { get; set; }
        //public string EmailsAddressFROM { get; set; }
        public string Subject { get; set; }
        public string Messagge { get; set; }
        public string Container { get; set; }
        public Boolean PriorityHigh { get; set; }
        public List<string> NombresArchivos { get; set; }
    }
}