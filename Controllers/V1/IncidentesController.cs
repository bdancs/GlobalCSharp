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
    public class IncidentesController : ControllerBase
    {
        private readonly SecurityContext _context;

        public IncidentesController(SecurityContext context)
        {
            _context = context;
        }

        // GET: api/v1/incidentes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _context.Incidentes
                .Include(i => i.Agente)
                .ToListAsync();

            return Ok(list);
        }

        // GET: api/v1/incidentes/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var incidente = await _context.Incidentes
                .Include(i => i.Agente)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (incidente == null) return NotFound();
            return Ok(incidente);
        }

        // POST: api/v1/incidentes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IncidenteDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Check if Agente exists
            var agenteExists = await _context.Agentes.AnyAsync(a => a.Id == dto.AgenteId);
            if (!agenteExists) return BadRequest($"Agente with id {dto.AgenteId} not found.");

            var incidente = new Incidente
            {
                Tipo = dto.Tipo,
                Descricao = dto.Descricao,
                Data = dto.Data,
                AgenteId = dto.AgenteId
            };

            _context.Incidentes.Add(incidente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = incidente.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0" }, incidente);
        }

        // PUT: api/v1/incidentes/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] IncidenteDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _context.Incidentes.AnyAsync(i => i.Id == id)) return NotFound();

            var incidente = await _context.Incidentes.FindAsync(id);
            incidente.Tipo = dto.Tipo;
            incidente.Descricao = dto.Descricao;
            incidente.Data = dto.Data;
            incidente.AgenteId = dto.AgenteId;

            _context.Entry(incidente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency issue while updating incidente.");
            }

            return NoContent();
        }

        // DELETE: api/v1/incidentes/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var incidente = await _context.Incidentes.FindAsync(id);
            if (incidente == null) return NotFound();

            _context.Incidentes.Remove(incidente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
