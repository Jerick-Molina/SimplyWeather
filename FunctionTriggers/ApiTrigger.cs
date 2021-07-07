using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Company.Function
{
    public static class ApiTrigger
    {
        static HttpClient client = new HttpClient(){};

       
       public class Req_Weather {

            public string name {get;set;}

            public int timezone {get;set;}
            public List<Weather> weather = new List<Weather>();
            public Main main;

            public Sys _system {get;set;}
            public class Weather {
                
                public int id {get;set;}
                public string main {get;set;}
            }

            public class Main {

               
            public double temp {get;set;}

            public double temp_max {get;set;}

            public double temp_min {get;set;}
            }

            public class Sys {

            }
        }

        public class Res_Weather{


            public string City {get;set;}
            
            public int timezone {get;set;}
            public _Weather Weather;
            public class _Weather {

                public int id {get;set;}
                  public double Temp {get;set;}
                public double mx_Temp {get;set;}

                public double mn_Temp {get;set;}
                public string cur_Weather {get;set;}

            }
        }
        
        [FunctionName("ApiTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Req_Weather>(requestBody);

            
            var res_Weather = new Res_Weather{
                City = data.name,

                Weather = new Res_Weather._Weather{
                    id = data.weather[0].id,
                    Temp = data.main.temp,
                    mx_Temp = data.main.temp_max,
                mn_Temp = data.main.temp_min,
                    cur_Weather = data.weather[0].main
                }
            };
       
            string ser_Json = JsonConvert.SerializeObject(res_Weather);
    
            return new OkObjectResult(ser_Json);
        }
    }
} 
