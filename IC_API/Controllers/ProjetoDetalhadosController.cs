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
    public class ProjetoDetalhadosController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProjetoDetalhadosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/ProjetoDetalhados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoDetalhado>>> GetProjetoDetalhado()
        {
            return await _context.ProjetoDetalhado.ToListAsync();
        }

        // GET: api/ProjetoDetalhados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoDetalhado>> GetProjetoDetalhado(int id)
        {
            var projetoDetalhado = await _context.ProjetoDetalhado.FindAsync(id);

            if (projetoDetalhado == null)
            {
                return NotFound();
            }

            return projetoDetalhado;
        }

        // PUT: api/ProjetoDetalhados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjetoDetalhado(int id, ProjetoDetalhado projetoDetalhado)
        {
            if (id != projetoDetalhado.id)
            {
                return BadRequest();
            }

            _context.Entry(projetoDetalhado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetoDetalhadoExists(id))
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

        // POST: api/ProjetoDetalhados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjetoDetalhado>> PostProjetoDetalhado(ProjetoDetalhado projetoDetalhado)
        {
            _context.ProjetoDetalhado.Add(projetoDetalhado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjetoDetalhadoExists(projetoDetalhado.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjetoDetalhado", new { id = projetoDetalhado.id }, projetoDetalhado);
        }

        // DELETE: api/ProjetoDetalhados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjetoDetalhado(int id)
        {
            var projetoDetalhado = await _context.ProjetoDetalhado.FindAsync(id);
            if (projetoDetalhado == null)
            {
                return NotFound();
            }

            _context.ProjetoDetalhado.Remove(projetoDetalhado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjetoDetalhadoExists(int id)
        {
            return _context.ProjetoDetalhado.Any(e => e.id == id);
        }
    }
}
