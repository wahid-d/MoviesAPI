using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Models;
using Movies.Models.Data;
using Movies.ViewModels;

namespace Movies.Controllers
{

    // TO-DO:
    // HTTP PATCH for changing a single movie
    // HTTP delete for changing a single movie

    // add Actors contgroller with the same actions as Movies controller

    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> mLogger;
        private readonly MoviesDbContext mContext;

        public MoviesController(ILogger<MoviesController> logger, MoviesDbContext context)
        {
            mLogger = logger;
            mContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await mContext.Movies.ToListAsync();

            return Ok(movies);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] List<AddMovieViewModel> movies)
        {

            if (movies == null || !movies.Any())
            {
                mLogger.LogInformation($"Empty data posted to Add action at {nameof(MoviesController)}");
                return BadRequest();
            }

            var newMovies = movies.Select(m =>
            new Movie(m.Title,
                m.ReleaseDate,
                m.Imdb,
                m.Description));

            await mContext.Movies.AddRangeAsync(newMovies);
            await mContext.SaveChangesAsync();

            return Accepted();
        }

        [HttpPatch]
        public IActionResult Change([FromQuery] Guid id)
        {
            return Ok(id);
        }
    }
}
