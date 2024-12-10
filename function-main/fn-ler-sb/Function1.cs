using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace fn_ler_sb
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly HttpClient _httpClient;
        public Function1(ILogger<Function1> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [Function(nameof(Function1))]
        public async Task Run(
            [ServiceBusTrigger("logicapp", Connection = "")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var jsonContent = message.Body.ToString();
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var url = "";
            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Success");

                //Complete the message
                await messageActions.CompleteMessageAsync(message);
            }
            else
            {
                _logger.LogInformation("Fail");
                await messageActions.DeferMessageAsync(message);
            }

        }
    }
}
