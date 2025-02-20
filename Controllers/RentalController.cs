using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoshopAPIV3.Data;
using VideoshopAPIV3.Model;

namespace VideoshopAPIV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public RentalController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalHeader>>> GetAllRentall()
        {
            var rental = await dbContext.RentalHeaders
                        .Include(c => c.Customers)
                        .Include(r => r.RentalDetails)
                        .ThenInclude(m => m.Movie)
                        .ToListAsync();

            return Ok(rental);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalHeader>> GetRentalById(int id)
        {
            var rental = await dbContext.RentalHeaders
                        .Include(c => c.Customers)
                        .Include(r => r.RentalDetails)
                        .ThenInclude(m => m.Movie)
                        .FirstOrDefaultAsync(r => r.Id == id);
            if (rental == null)
            {
                return BadRequest();
            }
            return Ok(rental);
        }
            


        [HttpPost]
        public async Task<IActionResult> CreateRental(int id, List<int> movieIds)
        {
            if (movieIds == null || movieIds.Count == 0)
            {
                return BadRequest("Choose at least one movie.");
            }

            var movies = await dbContext.Movies.Where(m => movieIds.Contains(m.Id)).ToListAsync();

            if (movies.Count != movieIds.Count)
                return NotFound("Some movies are not found.");

            var rentalHeader = new RentalHeader
            {
                CustomerId = id,
                RentalDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddDays(7)
            };

            dbContext.RentalHeaders.Add(rentalHeader);
            await dbContext.SaveChangesAsync(); // Save RentalHeader to get its Id

            // Create RentalDetail entries for each movie
            foreach (var movie in movies)
            {
                var rentalDetail = new RentalDetail
                {
                    MovieId = movie.Id,
                    RentalHeaderId = rentalHeader.Id
                };
                dbContext.RentalDetails.Add(rentalDetail);
            }

            await dbContext.SaveChangesAsync();

            return Ok(rentalHeader);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRental(int id)
        {
            var rental = await dbContext.RentalHeaders.FindAsync(id);
            if (rental == null) 
            {
                return BadRequest();
            }

            dbContext.RentalHeaders.Remove(rental);
            await dbContext.SaveChangesAsync();
            return Ok(rental);  
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditRental(int id, RentalHeader rentalHeader)
        {
            if (id != rentalHeader.Id)
            {
                return BadRequest();
            }
            dbContext.Entry(rentalHeader).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return Ok(rentalHeader);
        }    
    }
}
