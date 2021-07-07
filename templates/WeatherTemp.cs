using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public  class WeatherTemp
    {
            public class WeatherResponse {

            public string name {get;set;}

            public Weather _weather {get;set;}
            public Main _main {get;set;}

            public Sys _system {get;set;}
            public class Weather {

                public string main {get;set;}
            }

            public class Main {
             public float temp {get;set;}

            public float mx_temp {get;set;}

            public float mn_temp {get;set;}
            }

            public class Sys {

            }
        }
    }
}
