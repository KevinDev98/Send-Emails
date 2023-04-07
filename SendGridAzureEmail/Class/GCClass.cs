using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using SendGridAzureEmail.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SendGridAzureEmail.Class
{
    public class GCClass
    {
        public static async Task<Event> CreateGoogleCalendar(GoogleCalendarModel request)
        {//Le avisa al correo que allá dado de alta LA API
            DateTime StartT= DateTime.Now;
            string[] Scopes = { "https://www.googleapis.com/auth/calendar" };
            string ApplicationName = "Google Canlendar Api";
            UserCredential credential;
            //string pathjson = Path.Combine(Environment.CurrentDirectory, "CredentialGoogle.json");
            string pathjson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CredentialsGoogle.json");
            using (var stream = new FileStream(pathjson, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                try
                {
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                        GoogleClientSecrets.Load(stream).Secrets,
                                        Scopes,
                                        "user",
                                        CancellationToken.None,
                                        new FileDataStore(credPath, false)).Result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            // define services
            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
                
            });
            // define request
            Event eventCalendar = new Event()
            {
                Summary = request.Summary,
                Location = request.Location,
                //Attendees=request.Inivitados,

                Start = new EventDateTime
                {
                    DateTime = StartT,
                    TimeZone = "America/Mexico_City"
                },
                End = new EventDateTime
                {
                    DateTime = DateTime.Now,
                    TimeZone = "America/Mexico_City"
                },
                Description = request.Description
            };
            var eventRequest = services.Events.Insert(eventCalendar, "primary");
            var requestCreate = await eventRequest.ExecuteAsync();
            return requestCreate;
        }
    }
}