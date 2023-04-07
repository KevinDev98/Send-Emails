using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendGridAzureEmail.Models
{
    public class GoogleCalendarModel
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public IList<EventAttendee> Inivitados { get; set; }
    }
}