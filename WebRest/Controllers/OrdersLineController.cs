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
    public class OrdersLinesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrdersLinesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrdersLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> Get()
        {
            return await _context.OrdersLines.ToListAsync();
        }

        // GET: api/OrdersLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersLine>> Get(string id)
        {
            var OrdersLine = await _context.OrdersLines.FindAsync(id);

            if (OrdersLine == null)
            {
                return NotFound();
            }

            return OrdersLine;
        }

        // PUT: api/OrdersLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrdersLine OrdersLine)
        {
            if (id != OrdersLine.OrdersLineId)
            {
                return BadRequest();
            }

            _context.Entry(OrdersLine).State = EntityState.Modified;

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

        // POST: api/OrdersLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdersLine>> Post(OrdersLine OrdersLine)
        {
            _context.OrdersLines.Add(OrdersLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = OrdersLine.OrdersLineId }, OrdersLine);
        }

        // DELETE: api/OrdersLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var OrdersLine = await _context.OrdersLines.FindAsync(id);
            if (OrdersLine == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(OrdersLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.OrdersLines.Any(e => e.OrdersLineId == id);
        }
    }
}
