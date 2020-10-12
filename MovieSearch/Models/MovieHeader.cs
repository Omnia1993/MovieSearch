using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSearch.Models
{
    public class MovieHeader
    {
        public string searchType { get; set; }
        public string expression { get; set; }
        public Movie[] results { get; set; }
        public string errorMessage { get; set; }
    }
}
