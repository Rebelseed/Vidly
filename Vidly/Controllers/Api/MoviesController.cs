using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;


namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IHttpActionResult GetMovies()
        {
            var movieDtos = _context.Movies.Include(m => m.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        // GET /api/movies/id   
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies
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
