﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

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