using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;

namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public AddressesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> Get()
        {
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Get(string id)
        {
            var Address = await _context.Addresses.FindAsync(id);

            if (Address == null)
            {
                return NotFound();
            }

            return Address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Address Address)
        {
            if (id != Address.AddressId)
            {
                return BadRequest();
            }

            _context.Entry(Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> Post(Address Address)
        {
            _context.Addresses.Add(Address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = Address.AddressId }, Address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Address = await _context.Addresses.FindAsync(id);
            if (Address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(Address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
