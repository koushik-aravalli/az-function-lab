using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace az203.labs.Function
{
    public static class Lab01_ServicebusQueuetrigger
    {
        [FunctionName("Lab01_ServicebusQueuetrigger")]
        public static void Run([ServiceBusTrigger("samplequeue", Connection = "AzureServiceBusConnectionString")] string myqueue, ILogger log)
        {
            log.LogInformation($"Servicebus Message: {myqueue}");
        }
    }

}
