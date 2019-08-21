using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace az203.labs.Function
{
    public static class Lab01_Timertrigger
    {
        [FunctionName("Lab01_Timertrigger")]
        [return: ServiceBus("samplequeue", Connection = "AzureServiceBusConnectionString")]
        public static Location Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            Random random = new Random();

            return new Location()
            {
                Latitude = random.Next(516400146, 630304598).ToString(),
                Longitude = random.Next(224464416, 341194152).ToString(),
                TimeStamp = DateTime.Now.ToString(),
            };

        }
    }

    public class Location
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeStamp { get; set; }
    }
}
