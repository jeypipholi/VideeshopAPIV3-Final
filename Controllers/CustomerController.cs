using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoshopAPIV3.Data;
using VideoshopAPIV3.Model;

namespace VideoshopAPIV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public CustomerController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomer()
        {
            var customer = await dbContext.Customers
                        .Include(m => m.RentalHeaders)
                        .Include(r => r.RentalDetail)
                        .ToListAsync();
            return Ok(customer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByID(int id)
        {
            var customer = await dbContext.Customers
                         .FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer([FromBody] Customer customer)
        {
            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerByID), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            dbContext.Entry(customer).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return Ok(customer);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await dbContext.Customers.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }
            dbContext.Customers.Remove(customer);
            await dbContext.SaveChangesAsync();
            return Ok(customer);
        }
    }
}
