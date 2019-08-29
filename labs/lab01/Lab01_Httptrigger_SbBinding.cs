using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace az203.labs.Function
{
    public static class Lab01_Httptrigger_SbBinding
    {
        [FunctionName("Lab01_Httptrigger_SbBinding")]
        [return: ServiceBus("samplequeue", Connection = "AzureServiceBusConnectionString")]
        public static Location Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Ondemand HTTP request.");

            Random random = new Random();

            return new Location()
            {
                Latitude = random.Next(516400146, 630304598).ToString(),
                Longitude = random.Next(224464416, 341194152).ToString(),
                TimeStamp = DateTime.Now.ToString(),
            };
        }
    }
}
