using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.NotificationHubs;

namespace CallforHelp.SendNotificationFunction
{
    public static class HttpTriggerSendNotification
    {
        [FunctionName("HttpTriggerSendNotification")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";
            
            var notificationHubConnectionString = Environment.GetEnvironmentVariable("NotificationHubConnectionString");
            var notificationHubPath = Environment.GetEnvironmentVariable("NotificationHubPath");

            NotificationHubClient hub =
                NotificationHubClient.CreateClientFromConnectionString(notificationHubConnectionString, notificationHubPath);

            string reqMsg = req.Query["Message"];
            string defaultMsg = Environment.GetEnvironmentVariable("DefaultNotificationMessage");;
            string message = string.IsNullOrEmpty(reqMsg) ? defaultMsg : reqMsg;

            string tag = req.Query["RegId"];

            // Define an Anroid alert.
            var androidAlert = "{\"data\":{\"message\": \"" + message + "\"}}";

            hub.SendFcmNativeNotificationAsync(androidAlert,tag).Wait();

            return new OkObjectResult(responseMessage);
        }
    }
}
