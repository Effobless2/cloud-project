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

namespace FunctionApp1
{
    public static class Function1
    {
        private static string apiUrl = "https://esgi-4al1-group03-cloud-project-api.azurewebsites.net/api/todo";
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "put", "patch", "delete", Route = null)] HttpRequest req,
            ILogger log)
        {


            HttpClient httpClient = new HttpClient();
            HttpResponseMessage result = null;
            switch (req.Method)
            {
                case "GET":
                    string id = await GetId(req);
                    result = httpClient.GetAsync(apiUrl + "/" + id).Result;
                    break;
                case "PUT":
                    ByteArrayContent todo = await GetSerializedTodo(req);
                    result = await httpClient.PutAsync(apiUrl, todo);
                    break;
                case "PATCH":
                    todo = await GetSerializedTodo(req);
                    
                    if (todo.Headers.ContentLength > 0)
                    {
                        result = await httpClient.PatchAsync(apiUrl, todo);
                    }
                    else
                    {
                        id = await GetId(req);
                        result = await httpClient.PatchAsync(apiUrl + "/Check/" + id, new ByteArrayContent(new byte[0]));
                    }
                    break;
                case "DELETE":
                    id = await GetId(req);
                    result = await httpClient.DeleteAsync(apiUrl + "/" + id);
                    break;

            }

            return result;
        }

        private static async Task<ByteArrayContent> GetSerializedTodo(HttpRequest request)
        {
            string json = await request.ReadAsStringAsync();
            var buf = System.Text.Encoding.UTF8.GetBytes(json);
            var bytes = new ByteArrayContent(buf);
            bytes.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return bytes;
        }

        private static async Task<string> GetId(HttpRequest request)
        {
            string id = request.Query["id"];

            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            id = id ?? data?.id;
            return id;
        }
    }
}
