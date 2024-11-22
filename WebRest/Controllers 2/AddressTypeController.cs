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
    public class AddressTypesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public AddressTypesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/AddressTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> Get()
        {
            return await _context.AddressTypes.ToListAsync();
        }

        // GET: api/AddressTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressType>> Get(string id)
        {
            var AddressType = await _context.AddressTypes.FindAsync(id);

            if (AddressType == null)
            {
                return NotFound();
            }

            return AddressType;
        }

        // PUT: api/AddressTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, AddressType AddressType)
        {
            if (id != AddressType.AddressTypeId)
            {
                return BadRequest();
            }

            _context.Entry(AddressType).State = EntityState.Modified;

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

        // POST: api/AddressTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressType>> Post(AddressType AddressType)
        {
            _context.AddressTypes.Add(AddressType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = AddressType.AddressTypeId }, AddressType);
        }

        // DELETE: api/AddressTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var AddressType = await _context.AddressTypes.FindAsync(id);
            if (AddressType == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(AddressType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.AddressTypes.Any(e => e.AddressTypeId == id);
        }
    }
}
