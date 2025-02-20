using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoshopAPIV3.Data;
using VideoshopAPIV3.Model;

namespace VideoshopAPIV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public MovieController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var movies = await dbContext.Movies
                        .Include(r => r.RentalDetails)
                        .ToListAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie([FromBody] Movie movie)
        {
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(AddMovie), new { id = movie.Id }, movie);
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            dbContext.Entry(movie).State = EntityState.Modified;
            dbContext.SaveChanges();
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await dbContext.Movies.FindAsync(id);

            if (movie is null)
            {
                return BadRequest();
            }
            if (id != movie.Id)
            {
                return NotFound();
            }
            dbContext.Remove(movie);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
