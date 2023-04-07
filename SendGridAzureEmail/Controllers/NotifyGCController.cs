using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SendGridAzureEmail.Class;
using SendGridAzureEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SendGridAzureEmail.Controllers
{
    public class NotifyGCController : ApiController
    {
        public async Task<Google.Apis.Calendar.v3.Data.Event> NotifyGCalendar([FromBody] GoogleCalendarModel request)
        {
            return await GCClass.CreateGoogleCalendar(request);
        }
    }
}
