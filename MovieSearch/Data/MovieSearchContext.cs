using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieSearch.Models;

namespace MovieSearch.Data
{
    public class MovieSearchContext : DbContext
    {
        public MovieSearchContext (DbContextOptions<MovieSearchContext> options)
            : base(options)
        {
        }

        public DbSet<MovieSearch.Models.Movie> Movie { get; set; }

        public DbSet<MovieSearch.Models.TodoItem> TodoItem { get; set; }
    }
}
