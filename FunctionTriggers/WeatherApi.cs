using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Company.Function
{   
    public static class WeatherApi
    {   
        static HttpClient client = new HttpClient(){

            BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather")
        };
         
         public class ClientRequest{
             public string City {get;set;}
         }

         public class ApiResponse{

             public string cod {get;set;}
             public string message {get;set;}
         }


        [FunctionName("WeatherApi")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var clientURI = "http://localhost:7071/api/ApiTrigger";
            var query = "?";
            

            // Gets Request from  Client
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var test = JsonConvert.DeserializeObject<ClientRequest>(requestBody);
            var values = new Dictionary<String,String>
            {

                {$"q",$"{test.City}"},
                {"appid","271e2bf30bad893931a464ed508e5e41"},
                {"units","imperial"}
            };

       

            //Creates query.
            
            
            var dictonaryCount = 0;
            foreach(var value in values){
                for(int i = dictonaryCount; i < values.Count;){
                    if(i  < values.Count - 1){
                        query += value.Key + "=" + value.Value + "&";
                    }
                    else
                    {
                        query += value.Key + "=" + value.Value;
                    }
                     dictonaryCount += 1;
                    break;
                }
            }
            // Returns Weather Api response
            HttpResponseMessage api_return = await client.GetAsync(query);
        
            ApiResponse api_status = await api_return.Content.ReadAsAsync<ApiResponse>();

            var str_Response = await api_return.Content.ReadAsStringAsync();
            var http_Content = new StringContent(str_Response);

            Console.WriteLine(str_Response);
            if(api_status.cod == "404"){
                return new OkObjectResult(api_status);
            }else if (api_status.cod == "400"){
                 return new OkObjectResult(api_status); 
            }else if (api_status.cod == "401"){
                return new OkObjectResult(api_status); 
            }
          
            //Sends Api to function and gets Response.
            HttpResponseMessage func_Req = await client.PostAsync(clientURI,http_Content);

            var func_Res = await func_Req.Content.ReadAsStringAsync();
          

            return new OkObjectResult(func_Res);
        }
    }
}
