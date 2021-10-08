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
    public class TramitacoesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TramitacoesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Tramitacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tramitacao>>> GetTramitacao()
        {
            return await _context.Tramitacao.ToListAsync();
        }

        // GET: api/Tramitacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tramitacao>> GetTramitacao(int id)
        {
            var tramitacao = await _context.Tramitacao.FindAsync(id);

            if (tramitacao == null)
            {
                return NotFound();
            }

            return tramitacao;
        }

        // PUT: api/Tramitacoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTramitacao(int id, Tramitacao tramitacao)
        {
            if (id != tramitacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(tramitacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TramitacaoExists(id))
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

        // POST: api/Tramitacoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tramitacao>> PostTramitacao(Tramitacao tramitacao)
        {
            _context.Tramitacao.Add(tramitacao);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TramitacaoExists(tramitacao.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTramitacao", new { id = tramitacao.Id }, tramitacao);
        }

        // DELETE: api/Tramitacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTramitacao(int id)
        {
            var tramitacao = await _context.Tramitacao.FindAsync(id);
            if (tramitacao == null)
            {
                return NotFound();
            }

            _context.Tramitacao.Remove(tramitacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TramitacaoExists(int id)
        {
            return _context.Tramitacao.Any(e => e.Id == id);
        }
    }
}
