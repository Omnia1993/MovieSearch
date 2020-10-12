using System;
using System.IO;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MovieSearch.Models;

namespace MovieSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieAPI : ControllerBase
    {
        //  We just have a single API endpoint here. 
        //  provide a movie title and a list of JSON movies is return

        // GET: api/MovieApi/movie/inception
        [HttpGet("movie/{title}")]
        public ActionResult<Movie[]> GetMovie(String title)
        {
            //  this is my key. Register with IMDB and get your own
            String baseURL = "https://imdb-api.com/en/API/SearchMovie/k_lLeNEBFqX/";
            //  we are using the static MoviesController method GetJsonText to
            //  hit the IMDB movie API to get our list of movies (DRY)
            string movieResults = GetJsonText(baseURL + title);

            MovieHeader header = JsonSerializer.Deserialize<MovieHeader>(movieResults);

            return header.results;
        }

        // Returns JSON string
        /*
         * This little piece of software magic will make an AJAX call
         * to the API passed in
         * The request completes and the GetResponseStream method 
         * will provide the code with the text from the API
         */
        public static string GetJsonText(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                //  catch if the API request fails
                //  Log it if you like with the description of the error
                //  then throw the error (which will kill our application
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText if you want to 
                }
                throw;
            }
        }
    }
}
