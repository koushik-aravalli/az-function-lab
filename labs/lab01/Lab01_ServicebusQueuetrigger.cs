using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace az203.labs.Function
{
    public static class Lab01_ServicebusQueuetrigger
    {
        [FunctionName("Lab01_ServicebusQueuetrigger")]
        [return: TwilioSms(AccountSidSetting = "TwilioAccountSid", AuthTokenSetting = "TwilioAuthToken", From = "+3197014202316" )]
        public static async Task<CreateMessageOptions> RunAsync(
            [ServiceBusTrigger("samplequeue", Connection = "AzureServiceBusConnectionString")] string myqueue, 
            ILogger log)
        {
            log.LogInformation($"Servicebus Message: {myqueue}");

            var _service = new KeyVaultService();
            string secretValue = await _service.GetSecretValue(Environment.GetEnvironmentVariable("AzureKeyvaultSecret"));

            // Works for .csx file
            //var secretValue = Environment.GetEnvironmentVariable("AzureKv");

            log.LogInformation(secretValue);

            var message = new CreateMessageOptions(new PhoneNumber(Environment.GetEnvironmentVariable("OutgoingMobileNumber")))
            {
                Body = $"{myqueue}"
            };

            return message;
        }
    }

    public class KeyVaultService
    {
        public async Task<string> GetSecretValue(string keyName)
        {
            string secret = "";
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var secretBundle = await keyVaultClient.GetSecretAsync(Environment.GetEnvironmentVariable("AzureKeyvaultUri") + "secrets/" + keyName).ConfigureAwait(false);
            secret = secretBundle.Value;
            return secret;
        }

    }

}
