using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IC_API.Data;
using IC_API.Models;

namespace IC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegislaturasController : ControllerBase
    {
        private readonly AppDBContext _context;

        public LegislaturasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Legislaturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Legislatura>>> GetLegislatura()
        {
            return await _context.Legislatura.ToListAsync();
        }

        // GET: api/Legislaturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Legislatura>> GetLegislatura(int id)
        {
            var legislatura = await _context.Legislatura.FindAsync(id);

            if (legislatura == null)
            {
                return NotFound();
            }

            return legislatura;
        }

        // PUT: api/Legislaturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLegislatura(int id, Legislatura legislatura)
        {
            if (id != legislatura.id)
            {
                return BadRequest();
            }

            _context.Entry(legislatura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LegislaturaExists(id))
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

        // POST: api/Legislaturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Legislatura>> PostLegislatura(Legislatura legislatura)
        {
            _context.Legislatura.Add(legislatura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLegislatura", new { id = legislatura.id }, legislatura);
        }

        // DELETE: api/Legislaturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLegislatura(int id)
        {
            var legislatura = await _context.Legislatura.FindAsync(id);
            if (legislatura == null)
            {
                return NotFound();
            }

            _context.Legislatura.Remove(legislatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LegislaturaExists(int id)
        {
            return _context.Legislatura.Any(e => e.id == id);
        }
    }
}
