using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.ViewModels;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContextViewModel _context;

        public MoviesController()
        {
            _context = new ApplicationDbContextViewModel();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel 
            {
                Genre = genres
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genre = _context.Genres.ToList()
                };
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var MovieinDb = _context.Movies.Single(c => c.Id == movie.Id);

                MovieinDb.Name = movie.Name;
                MovieinDb.ReleaseDate = movie.ReleaseDate;
                MovieinDb.GenreId = movie.GenreId;
                MovieinDb.Stock = movie.Stock;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genre = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }
        }

        public ViewResult Index()
        {
            //This uncommented code uses the api/movies instead
            //var movies = _context.Movies.Include(m => m.Genre).ToList();

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        /*  public ActionResult Random()
       {
           var movie = new Movie { Name = "Shrek" };

           var customers = new List<Customer>
           {
               new Customer {Name = "John Smith" },
               new Customer {Name = "Mary Watson" }
           };

           var viewModel = new RandomMovieViewModel
           {
               Movie = movie,
               Customers = customers
           };

           return View(viewModel);
       }
       */

    }
}