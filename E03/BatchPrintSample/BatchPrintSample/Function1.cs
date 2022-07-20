using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BatchPrintSample
{
    public static class Function1
    {
        [FunctionName("BatchPrintFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            log.LogInformation("C# HTTP trigger function processed a request.");

            //var endpoint = "https://apiazewtmlns001sgproxy.azurewebsites.net/document/document-pouches/forwarders-cargo-receipts/FCR000010745/print";
            var endpoint = "https://api-test.scm.maersk.com/document/document-pouches/forwarders-cargo-receipts/FCR000010745/print";
            Uri baseUrl = new Uri(endpoint);
            var body = new { PrintOnlyCopy = true, PrintSignature = true };

            HttpResponseMessage response = null;
            var iteration = 0;
            var retryCount = 100;
            var client = new HttpClient();
            do
            {
                
                log.LogInformation($"processing request for : {baseUrl}");

                var msg = new HttpRequestMessage(HttpMethod.Post, baseUrl);
                msg.Headers.Add("Accept", "*/*");
                msg.Headers.Add("Authorization", $"Bearer {requestBody}");
                msg.Headers.Add("Application-Key", "BXfbHrTK+brHcIjWlFr5MXQYekscG+SAgXynaK93vTA=");
                msg.Headers.Add("Api-Version", "v2");
                msg.Content = new StringContent(JsonConvert.SerializeObject(body));
                msg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await client.SendAsync(msg);
                response.EnsureSuccessStatusCode();

                log.LogInformation($"Processed : {baseUrl} :: {iteration}");

                iteration++;
            } while (iteration < retryCount);


            return new OkObjectResult(response);
        }
    }
}
