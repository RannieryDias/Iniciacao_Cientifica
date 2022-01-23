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
    public class TemasController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TemasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Temas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tema>>> GetTema()
        {
            return await _context.Tema.ToListAsync();
        }

        // GET: api/Temas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tema>> GetTema(string id)
        {
            var tema = await _context.Tema.FindAsync(id);

            if (tema == null)
            {
                return NotFound();
            }

            return tema;
        }

        // PUT: api/Temas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTema(string id, Tema tema)
        {
            if (id != tema.cod)
            {
                return BadRequest();
            }

            _context.Entry(tema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemaExists(id))
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

        // POST: api/Temas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tema>> PostTema(Tema tema)
        {
            _context.Tema.Add(tema);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TemaExists(tema.cod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTema", new { id = tema.cod }, tema);
        }

        // DELETE: api/Temas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTema(string id)
        {
            var tema = await _context.Tema.FindAsync(id);
            if (tema == null)
            {
                return NotFound();
            }

            _context.Tema.Remove(tema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TemaExists(string id)
        {
            return _context.Tema.Any(e => e.cod == id);
        }
    }
}
