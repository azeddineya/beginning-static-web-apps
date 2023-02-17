using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Api
{
    public static class WeatherForecast
    {
        [FunctionName("WeatherForecast")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "weather-forecast/{daysToForecast=5}")] HttpRequest req,
            ILogger log, int daysToForecast)
        {
            return new OkObjectResult(GetWeather(daysToForecast));
        }
        private static dynamic[] GetWeather(int daysToForecast)
        {
            var enumerator = Enumerable.Range(1, daysToForecast);
            var result = new List<dynamic>();
            var rnd = new Random();
            foreach (var day in enumerator)
            {
                var temp = rnd.Next(25);
                var summary = GetSummary(temp);
                // var date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(day).ToString("yyyy-MM-dd");
                result.Add(new
                {
                    Date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(day).ToString("yyyy-MM-dd"),
                    Summary = summary,
                    TemperatureC = temp
                }); ;
            }
            dynamic[] test = result.ToArray();
            return result.ToArray();
        }
        private static string GetSummary(int temp)
        {
            return temp switch
            {
                int i when (i > 20) => "Hot",
                int i when (i > 15) => "Warm",
                int i when (i > 10) => "Cool",
                int i when (i > 5) => " Cold",
                _ => "Too cold!"
            };
        }
    }
}
