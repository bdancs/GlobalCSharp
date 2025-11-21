using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityState.API.Data;
using SecurityState.API.DTOs;
using SecurityState.API.Entities;

namespace SecurityState.API.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AgentesController : ControllerBase
    {
        private readonly SecurityContext _context;

        public AgentesController(SecurityContext context)
        {
            _context = context;
        }

        // GET: api/v1/agentes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var agentes = await _context.Agentes.ToListAsync();
            return Ok(agentes);
        }

        // GET: api/v1/agentes/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var agente = await _context.Agentes.FindAsync(id);
            if (agente == null) return NotFound();
            return Ok(agente);
        }

        // POST: api/v1/agentes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AgenteDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var agente = new Agente
            {
                Nome = dto.Nome,
                Funcao = dto.Funcao
            };

            _context.Agentes.Add(agente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = agente.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0" }, agente);
        }

        // PUT: api/v1/agentes/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] AgenteDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var agente = await _context.Agentes.FindAsync(id);
            if (agente == null) return NotFound();

            agente.Nome = dto.Nome;
            agente.Funcao = dto.Funcao;

            _context.Entry(agente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/v1/agentes/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var agente = await _context.Agentes
                .Include(a => a.Incidentes)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agente == null) return NotFound();

            if (agente.Incidentes != null && agente.Incidentes.Any())
                return BadRequest("Cannot delete agente with related incidents.");

            _context.Agentes.Remove(agente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
