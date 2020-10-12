using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieSearch.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly ILogger<ForecastController> _logger;

        public ForecastController(ILogger<ForecastController> logger)
        {
            _logger = logger;
        }

        // GET: api/<Forecast>
        /*
         *  Just a simple API. Browswer or Postman does a api/forecast GET 
         *  a 5 day JSON forecast is returned
         * 
         */
        [HttpGet]
        public IEnumerable<Forecast> Get()
        {
            string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var rng = new Random();         //  create a random number generator

            //  a bit of fancy C#. Each line is documented below
            //      return                      ultimately it returns an array of Forecast objects
            //      Enumerable.Range(1, 5)      create an array of numbers 1 to 5
            //                                  OK fine it is not an array it is an Enumerable list
            //      Select(index =>             takes each of those 5 numbers and applies it to the next line of code
            //      new Forecast                create a new Forecast object
            //      .ToArray();                 take each of those Forecast objects and add them to an array
            return Enumerable.Range(1, 5).Select(index => new Forecast
            {
                Date = DateTime.Now.AddDays(index),             //  create a date index days into the future
                TemperatureC = rng.Next(-20, 55),               //  random temp between -20 and 55 C
                Summary = Summaries[rng.Next(Summaries.Length)] //  random weather condition
            })
            .ToArray();
        }
    }
}
