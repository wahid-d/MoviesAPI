using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [Route("signin")]
        public IActionResult Signin()
        {
            var userEmail = "david@somewhere.com";
            var userRole = "superadmin";

            var token = JwtService.GenerateToken(userEmail, userRole);
            HttpContext.Session.SetString("JWT", token);

            return Ok(new
            {
                token = token,
                error = false
            });
        }

        [Authorize(Roles = "superadmin")]
        [HttpPatch]
        [Route("edit/{id}")]
        public async Task<IActionResult> Change([FromRoute] Guid id, [FromBody] ChangeMovieViewModel model, [FromQuery] string search)
        {
            if (!TryValidateModel(model))
            {
                return BadRequest();
            }

            Console.WriteLine(HttpContext.Request.Headers["Authorization"]);
            Console.WriteLine(JwtService.GetClaim(HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1]));

            //if (!await mContext.Movies.AnyAsync(m => m.ID == id))
            //{
            //    return NotFound(new { ID = id });
            //}

            //Movie movie = await mContext.Movies.FirstOrDefaultAsync(m => m.ID == id);

            //movie.Title = model.Title;
            //movie.ReleaseDate = model.ReleaseDate;

            //mContext.Movies.Update(movie);
            //await mContext.SaveChangesAsync();

            return Ok(id);
        }
    }
}
