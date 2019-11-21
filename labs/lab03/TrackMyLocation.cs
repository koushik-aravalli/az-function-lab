using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace lab03
{
    public static class TrackMyLocation
    {
        [FunctionName("TrackMyLocation")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>("Activity_1", "Koushik"));
            outputs.Add(await context.CallActivityAsync<string>("Activity_2", "Everyone"));
            outputs.Add(await context.CallActivityAsync<string>("Activity_3", "All"));

            return outputs;
        }

        [FunctionName("Activity_1")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }

        [FunctionName("Activity_2")]
        public static string Greet([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Greetings {name}.");
            return $"Greetings {name}!";
        }

        [FunctionName("Activity_3")]
        public static string SayBye([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"SayBye {name}.");
            return $"Goodbye {name}!";
        }

        [FunctionName("TrackMyLocation_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("TrackMyLocation", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}