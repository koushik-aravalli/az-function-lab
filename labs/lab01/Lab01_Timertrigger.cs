using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace az203.labs.Function
{
    public static class Lab01_Timertrigger
    {
        [FunctionName("Lab01_Timertrigger")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }

    public class Location{
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
