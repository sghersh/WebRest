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
    public class OrderStatesController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrderStatesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderState>>> Get()
        {
            return await _context.OrderStates.ToListAsync();
        }

        // GET: api/OrderStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderState>> Get(string id)
        {
            var OrderState = await _context.OrderStates.FindAsync(id);

            if (OrderState == null)
            {
                return NotFound();
            }

            return OrderState;
        }

        // PUT: api/OrderStates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderState OrderState)
        {
            if (id != OrderState.OrderStateId)
            {
                return BadRequest();
            }

            _context.Entry(OrderState).State = EntityState.Modified;

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

        // POST: api/OrderStates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderState>> Post(OrderState OrderState)
        {
            _context.OrderStates.Add(OrderState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = OrderState.OrderStateId }, OrderState);
        }

        // DELETE: api/OrderStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var OrderState = await _context.OrderStates.FindAsync(id);
            if (OrderState == null)
            {
                return NotFound();
            }

            _context.OrderStates.Remove(OrderState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(string id)
        {
            return _context.OrderStates.Any(e => e.OrderStateId == id);
        }
    }
}
