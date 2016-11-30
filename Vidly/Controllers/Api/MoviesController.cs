using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.ViewModels;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;
using Vidly.Models;


namespace Vidly.Controllers.Api
{
    //[Authorize(Roles = RoleName.CanManageMovies)]
    public class MoviesController : ApiController
    {

        private ApplicationDbContextViewModel _context;

        public MoviesController()
        {
            _context = new ApplicationDbContextViewModel();
        }

        // GET /api/movies
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!string.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        // GET /api/movies/id
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/id
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieinDB = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieinDB == null)
                return NotFound();

            Mapper.Map(movieDto, movieinDB);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/movies/id
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieinDB = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieinDB == null)
                return NotFound();

            _context.Movies.Remove(movieinDB);
            _context.SaveChanges();

            return Ok();
        }
    }
}
