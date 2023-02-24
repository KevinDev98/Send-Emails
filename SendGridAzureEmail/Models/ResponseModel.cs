using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SendGridAzureEmail.Models
{
    public class ResponseModel
    {
        public int CodeResponse { get; set; }
        public string MessageResponse { get; set; }
    }
}