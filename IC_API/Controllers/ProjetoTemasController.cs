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
    public class ProjetoTemasController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProjetoTemasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/ProjetoTemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoTema>>> GetProjetoTema()
        {
            return await _context.ProjetoTema.ToListAsync();
        }

        // GET: api/ProjetoTemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoTema>> GetProjetoTema(int id)
        {
            var projetoTema = await _context.ProjetoTema.FindAsync(id);

            if (projetoTema == null)
            {
                return NotFound();
            }

            return projetoTema;
        }

        // PUT: api/ProjetoTemas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjetoTema(int id, ProjetoTema projetoTema)
        {
            if (id != projetoTema.idProjeto)
            {
                return BadRequest();
            }

            _context.Entry(projetoTema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetoTemaExists(id))
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

        // POST: api/ProjetoTemas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjetoTema>> PostProjetoTema(ProjetoTema projetoTema)
        {
            _context.ProjetoTema.Add(projetoTema);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjetoTemaExists(projetoTema.idProjeto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjetoTema", new { id = projetoTema.idProjeto }, projetoTema);
        }

        // DELETE: api/ProjetoTemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjetoTema(int id)
        {
            var projetoTema = await _context.ProjetoTema.FindAsync(id);
            if (projetoTema == null)
            {
                return NotFound();
            }

            _context.ProjetoTema.Remove(projetoTema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjetoTemaExists(int id)
        {
            return _context.ProjetoTema.Any(e => e.idProjeto == id);
        }
    }
}
