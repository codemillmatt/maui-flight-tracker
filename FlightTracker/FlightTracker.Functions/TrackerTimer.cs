using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using FlightTracker.Models;

namespace FlightTracker.Functions
{
    public class TrackerTimer
    {
        static HttpClient client = new HttpClient();

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate( 
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "flighttracker")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("broadcast")]
        public async Task Run([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer, 
        [SignalR(HubName = "flighttracker")] IAsyncCollector<SignalRMessage> signalRMessages,
        ILogger log)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://adsbx-flight-sim-traffic.p.rapidapi.com/api/aircraft/json/lat/47.6205/lon/-122.3493/dist/25/"),
                Headers =
                {
                    { "X-RapidAPI-Key", Environment.GetEnvironmentVariable("RapidAPIKey") },
                    { "X-RapidAPI-Host", Environment.GetEnvironmentVariable("RapidAPIHost")  },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                await signalRMessages.AddAsync(
                    new SignalRMessage
                    {
                        Target = "newMessage",
                        Arguments = new[] { body }
                    });
            }
        }
    }
}
