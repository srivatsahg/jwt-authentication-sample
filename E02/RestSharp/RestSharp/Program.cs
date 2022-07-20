using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestSharp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            //var endpoint = "https://apiazewtmlns001sgproxy.azurewebsites.net/document/document-pouches/forwarders-cargo-receipts/FCR000010745/print";
            var endpoint = "https://api-test.scm.maersk.com/document/document-pouches/forwarders-cargo-receipts/FCR000010745/print";
            Uri baseUrl = new Uri(endpoint);
            var body = new { PrintOnlyCopy = true, PrintSignature = true };

            //RestClient client = new RestClient(baseUrl);
            //RestRequest request = new RestRequest(baseUrl, Method.Post);
            //request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSJ9.eyJhdWQiOiIwOWUyNTU5My1mNWU3LTQ1M2QtYTFhNy02MTQ0MjNkMDdlNjIiLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vMDVkNzVjMDUtZmExYS00MmU3LTljZjEtZWI0MTZjMzk2ZjJkL3YyLjAiLCJpYXQiOjE2NTY0OTk4NTQsIm5iZiI6MTY1NjQ5OTg1NCwiZXhwIjoxNjU2NTAzNzU0LCJhaW8iOiJBWFFBaS84VEFBQUFCckFvN1ZHMjJIZk1ZUHArYmxyajNHUWliNit6VCs5N3ZMNTVrRjVTK3VlMkk2cFhBdWQvRWxja1VuRWkzaTlxRDVab3NMbU1hOFl6bHBYaHA4ZUlzOG9YK1VpNGdTMVp6SFFYQ253SUlPbXRlTXBLYzJlN1pvbXd1Sk5JYlRyc09HcHFEZXNVdlRNUmN5NGhpNWFkdkE9PSIsIm5hbWUiOiJTcml2YXRzYSBHdXJ1cmFqIEhhcmlkYXMiLCJub25jZSI6ImQzNDJhY2EwLTcxZWItNGRlZS05NWNhLTdlY2Y2NzI5OTBlYyIsIm9pZCI6ImE2MDQ2YWRjLWQ3OGQtNDIyMi1iYjIwLTlmYTBhZDU5ZmJlZiIsInByZWZlcnJlZF91c2VybmFtZSI6InNyaXZhdHNhLmhhcmlkYXNAbWFlcnNrLmNvbSIsInJoIjoiMC5BUkFBQlZ6WEJScjY1MEtjOGV0QmJEbHZMWk5WNGdubjlUMUZvYWRoUkNQUWZtSVFBSW8uIiwic3ViIjoieGxGYWRyLWM2TVJuT0RWNmVlenlCd3M5M1RZSDEzZ2tiaUhfMzVIYWxydyIsInRpZCI6IjA1ZDc1YzA1LWZhMWEtNDJlNy05Y2YxLWViNDE2YzM5NmYyZCIsInVwbiI6InNyaXZhdHNhLmhhcmlkYXNAbWFlcnNrLmNvbSIsInV0aSI6IkgzdGZqWmFvVUVTRU5na3FhMzVUQUEiLCJ2ZXIiOiIyLjAifQ.LiLYtRdIyGUAHYRlWGJqqrBCJs_Zr5WHDAA512RBZCckGrT69S2EvWTZ0QB3j-MrzQOQEnzFVN5LdN9x5AsptA6p5rvNrTfrhp8CwOo0WAnXCEPYWsV2rKlHh8hAgdS-9Gz9G6eugeXY11519s_UEh6d5ZFx-rRs8nbQaHM9AEiOb4s_nCOEV9tUd1ZEM50sNh6lAECV2i1Pn5nsIHbNxlpsw6SAfbx3NzhtJEEQ7_0UNhnnHI8pQia_MJCt0qKKpFnmjYYG475dKjdxCuTzRzzU_dZJ_ocVZGxYEoiiO5oPoq4eRutrvguVDUsqbrSFwGwHUPCZOaAo7oamQCBQoA");
            //request.AddHeader("Application-Key", "BXfbHrTK+brHcIjWlFr5MXQYekscG+SAgXynaK93vTA=");
            //request.AddHeader("Api-Version", "v2");
            //request.AddHeader("Content-Type", "application/json; charset=utf-8");
            //request.AddBody(JsonConvert.SerializeObject(body));
            //RestResponse response = null;

            HttpResponseMessage response = null;
            var iteration = 0;
            var retryCount = 100;
            var client = new HttpClient();
            do
            {
                Console.WriteLine($">> ::  {baseUrl} :: {iteration}");

                var msg = new HttpRequestMessage(HttpMethod.Post, baseUrl);
                msg.Headers.Add("Accept", "*/*");
                msg.Headers.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSJ9.eyJhdWQiOiIwOWUyNTU5My1mNWU3LTQ1M2QtYTFhNy02MTQ0MjNkMDdlNjIiLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vMDVkNzVjMDUtZmExYS00MmU3LTljZjEtZWI0MTZjMzk2ZjJkL3YyLjAiLCJpYXQiOjE2NTY1MDQ3MjMsIm5iZiI6MTY1NjUwNDcyMywiZXhwIjoxNjU2NTA4NjIzLCJhaW8iOiJBWFFBaS84VEFBQUFkZWRjeUpJUVlKeThESW03UVpMZFg5Y0lPUnpLeEpyQ0hkd2x6U3NyVmw5NjFDYmJiNnZTOWZTNHQwdGgrV3pJYng4YTcySkQ3RDJ2WEtIZ3BxOTA2b2Foa1dVTXVkNmZWMUJ1THU4bE5ka2w4Y2FzbGZQeW4xZy9CbjJ1YnpnRDI0a1dSSGdtRFVaeUNoUnFQWnMrVVE9PSIsIm5hbWUiOiJTcml2YXRzYSBHdXJ1cmFqIEhhcmlkYXMiLCJub25jZSI6IjQ4OTk5M2MzLTVlY2ItNGQ2NS04NWE1LWIxNzRhNjVjODIyYyIsIm9pZCI6ImE2MDQ2YWRjLWQ3OGQtNDIyMi1iYjIwLTlmYTBhZDU5ZmJlZiIsInByZWZlcnJlZF91c2VybmFtZSI6InNyaXZhdHNhLmhhcmlkYXNAbWFlcnNrLmNvbSIsInJoIjoiMC5BUkFBQlZ6WEJScjY1MEtjOGV0QmJEbHZMWk5WNGdubjlUMUZvYWRoUkNQUWZtSVFBSW8uIiwic3ViIjoieGxGYWRyLWM2TVJuT0RWNmVlenlCd3M5M1RZSDEzZ2tiaUhfMzVIYWxydyIsInRpZCI6IjA1ZDc1YzA1LWZhMWEtNDJlNy05Y2YxLWViNDE2YzM5NmYyZCIsInVwbiI6InNyaXZhdHNhLmhhcmlkYXNAbWFlcnNrLmNvbSIsInV0aSI6ImJCNmF2ejh5eEV1ZEdxZUNNYjVEQUEiLCJ2ZXIiOiIyLjAifQ.DEPVtf5w4PRNtaf7URW8QD62IJGgOA7VCuNVGDu9hDCoj3tvovaDNriCgMx4j7e47aX5iOuJ0YeM4-R0BjXHwL-nboHGslac7UTZNx4qoQviK1qzPBYADhWcTZLGF56nulXUphdCI7uLkvZqqK8wMraDBGO4nzi2M0Sb56TRSEBVKKVHYIJzcfjsLtMo6xzkpShgs9wbdBGT4rF5gHgjeM9FRyE0ihNlHaRXO-VSbi-UPmj_dhLp3Xt2QAf4B_0_GHOUuzeenDwQlkcUkJvJj2fCxYWwWpvsJWO7YPwuQDBuTMtlW00FgT3GhgVqi1_g0RbNzm2-2RoErg-GhGcysA");
                msg.Headers.Add("Application-Key", "BXfbHrTK+brHcIjWlFr5MXQYekscG+SAgXynaK93vTA="); 
                msg.Headers.Add("Api-Version", "v2");
                msg.Content = new StringContent(JsonConvert.SerializeObject(body));
                msg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await client.SendAsync(msg);
                response.EnsureSuccessStatusCode();
                
                iteration++;
            } while (iteration < retryCount);

            Console.ReadLine();
        }
    }
}