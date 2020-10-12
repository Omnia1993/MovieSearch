using System.ComponentModel.DataAnnotations;

namespace MovieSearch.Models
{
    public class Movie
    {
        public string id { get; set; }
        [Display(Name = "IMDB Result")]
        public string resultType { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
