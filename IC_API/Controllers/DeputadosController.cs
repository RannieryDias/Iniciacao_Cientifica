using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IC_API.Data;
using IC_API.Model;

namespace IC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeputadosController : ControllerBase
    {
        private readonly AppDBContext _context;

        public DeputadosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Deputados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deputado>>> GetDeputado()
        {
            return await _context.Deputado.ToListAsync();
        }

        // GET: api/Deputados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deputado>> GetDeputado(int id)
        {
            var deputado = await _context.Deputado.FindAsync(id);

            if (deputado == null)
            {
                return NotFound();
            }

            return deputado;
        }

        // PUT: api/Deputados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeputado(int id, Deputado deputado)
        {
            if (id != deputado.id)
            {
                return BadRequest();
            }

            _context.Entry(deputado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeputadoExists(id))
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

        // POST: api/Deputados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Deputado>> PostDeputado(Deputado deputado)
        {
            _context.Deputado.Add(deputado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeputadoExists(deputado.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDeputado", new { id = deputado.id }, deputado);
        }

        // DELETE: api/Deputados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeputado(int id)
        {
            var deputado = await _context.Deputado.FindAsync(id);
            if (deputado == null)
            {
                return NotFound();
            }

            _context.Deputado.Remove(deputado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeputadoExists(int id)
        {
            return _context.Deputado.Any(e => e.id == id);
        }
    }
}
