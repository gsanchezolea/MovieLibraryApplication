using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPISample.Data;
using WebAPISample.Models;

namespace WebAPISample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private ApplicationContext _context;
        public MovieController(ApplicationContext context)
        {
            _context = context;
        }
        // GET api/movie
        [HttpGet]
        public IActionResult Get()
        {
            //// Retrieve all movies from db logic  
            return Ok(_context.Movies);
        }

        // GET api/movie/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Retrieve movie by id from db logic
            return Ok(_context.Movies.SingleOrDefault(m => m.MovieId == id));
        }

        // POST api/movie
        [HttpPost]
        public IActionResult Post([FromBody]Movie value)
        {
            // Create movie in db logic
            var movie = new Movie()
            {
                Title = value.Title,
                Genre = value.Genre,
                Director = value.Director
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            var movieOut = _context.Movies.Where(m => m.Title == movie.Title).SingleOrDefault();

            return Ok(movieOut);
        }

        // PUT api/movie/5
        [HttpPut]
        public IActionResult Put([FromBody]Movie value)
        {
            // Update movie in db logic
            var dbMovie = _context.Movies.Find(value.MovieId);
            if (dbMovie == null)
            {
                return BadRequest();
            }
            if(value.Title != null)
            {
                dbMovie.Title = value.Title;
            }
            if (value.Genre != null)
            {
                dbMovie.Genre = value.Genre;
            }
            if (value.Director != null)
            {
                dbMovie.Director = value.Director;
            }
      
            _context.SaveChanges();

            return Ok(_context.Movies);
        }

        // DELETE api/movie/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Delete movie from db logic
            var dbMovie = _context.Movies.SingleOrDefault(m => m.MovieId == id);

            _context.Movies.Remove(dbMovie);
            _context.SaveChanges();

            return Ok(_context.Movies);
        }
    }
}