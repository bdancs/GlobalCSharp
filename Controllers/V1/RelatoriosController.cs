using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityState.API.Data;
using SecurityState.API.DTOs;
using SecurityState.API.Entities;

namespace SecurityState.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly SecurityContext _context;

        public RelatoriosController(SecurityContext context)
        {
            _context = context;
        }

        // GET: api/v1/relatorios
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rels = await _context.Relatorios
                .Include(r => r.Incidente)
                .ToListAsync();

            return Ok(rels);
        }

        // GET: api/v1/relatorios/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rel = await _context.Relatorios
                .Include(r => r.Incidente)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rel == null) return NotFound();
            return Ok(rel);
        }

        // POST: api/v1/relatorios
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RelatorioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // verify incident exists
            var incidentExists = await _context.Incidentes.AnyAsync(i => i.Id == dto.IncidenteId);
            if (!incidentExists) return BadRequest($"Incidente with id {dto.IncidenteId} not found.");

            var rel = new Relatorio
            {
                Conteudo = dto.Conteudo,
                IncidenteId = dto.IncidenteId,
                CriadoEm = dto.CriadoEm
            };

            _context.Relatorios.Add(rel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = rel.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0" }, rel);
        }

        // DELETE: api/v1/relatorios/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rel = await _context.Relatorios.FindAsync(id);
            if (rel == null) return NotFound();

            _context.Relatorios.Remove(rel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
