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
    public class ProductStatusesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public ProductStatusesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> Get()
        {
            return await _context.ProductStatuses.ToListAsync();
        }

        // GET: api/ProductStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStatus>> Get(string id)
        {
            var ProductStatus = await _context.ProductStatuses.FindAsync(id);

            if (ProductStatus == null)
            {
                return NotFound();
            }

            return ProductStatus;
        }

        // PUT: api/ProductStatuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductStatus ProductStatus)
        {
            if (id != ProductStatus.ProductStatusId)
            {
                return BadRequest();
            }

            _context.Entry(ProductStatus).State = EntityState.Modified;

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

        // POST: api/ProductStatuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductStatus>> Post(ProductStatus ProductStatus)
        {
            _context.ProductStatuses.Add(ProductStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = ProductStatus.ProductStatusId }, ProductStatus);
        }

        // DELETE: api/ProductStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ProductStatus = await _context.ProductStatuses.FindAsync(id);
            if (ProductStatus == null)
            {
                return NotFound();
            }

            _context.ProductStatuses.Remove(ProductStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
    }
}
