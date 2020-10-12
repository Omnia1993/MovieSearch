using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieSearch.Data;
using MovieSearch.Models;

namespace MovieSearch.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieSearchContext _context;

        public MoviesController(MovieSearchContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            // we have added a little bit of code to the Index page. If we are coming from the IMDB search page
            //      we only want to show the Add link. Coming from here we want to show Details | Edit | Delete
            ViewBag.AddOnly = false;
            return View(await _context.Movie.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,resultType,image,title,description")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,resultType,image,title,description")] Movie movie)
        {
            if (id != movie.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(string id)
        {
            return _context.Movie.Any(e => e.id == id);
        }

        // GET: Movies/CreateFrom
        /*
         *  The user has searched for a movie title.
         *  The search results page ("Search.cshtml") has an Add button
         *  clicking the add button will take the user to this endpoint
         *  which will display the attributes and 
         *  let the user save the movie to the project database. 
         *  
         *  The save button on the CreateFrom page will send Movie data
         *  to the Create method above. 
         *  Just like any Movie created from the Create link
         */
        public IActionResult CreateFrom([Bind("id,image,title,description")] Movie movie)
        {
            movie.resultType = "From IMDB";
            return View(movie);
        }

        // POST: Movies/Search/{title}
        /*
         *  The user has requested a search of IMDB
         *  the application will make an AJAX call to IMDB and
         *  return the results (10 movies).
         *  text returned is just text. 
         *  Use JsonSerializer to Deserialize the text to a POCO.
         *  The MovieHeader POCO has a few uninteresting attributes
         *  plus an array of Movie objects in the results attribute.
         *  header.results is the array of Movies
         *  The array is passed to the Search page 
         *  which will list the 10 movies including the movie poster
         */
        [HttpPost]
        public IActionResult Search(string title)
        {
            String baseURL = "https://imdb-api.com/en/API/SearchMovie/k_lLeNEBFq/";
            string movieResults = MovieAPI.GetJsonText(baseURL + title);

            // Put a break point here and look at the movieResults
            //  it is a long string of text that looks like JSON
            //  Deserializer will look at the text and the MovieHeader
            //  object and recognize that MovieHeader is the object version
            //  of this JSON text
            //  WOW! this line of code does very cool stuff!!!
            MovieHeader header = JsonSerializer.Deserialize<MovieHeader>(movieResults);
            //  we are reusing the Index page. The AddOnly variable says only show the Add link
            ViewBag.AddOnly = true;
            return View("Index", header.results);
        }
    }
}
