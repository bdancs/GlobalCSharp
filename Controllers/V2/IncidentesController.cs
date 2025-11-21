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
    public class IncidentesController : ControllerBase
    {
        private readonly SecurityContext _context;

        public IncidentesController(SecurityContext context)
        {
            _context = context;
        }

        // GET: api/v2/incidentes?tipo=Invasao&page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? tipo, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 10;

            var query = _context.Incidentes.Include(i => i.Agente).AsQueryable();

            if (!string.IsNullOrWhiteSpace(tipo))
                query = query.Where(i => i.Tipo.Contains(tipo));

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(i => i.Data)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
                Items = items
            };

            return Ok(result);
        }

        // GET: api/v2/incidentes/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var incidente = await _context.Incidentes.Include(i => i.Agente).FirstOrDefaultAsync(i => i.Id == id);
            if (incidente == null) return NotFound();
            return Ok(incidente);
        }

        // POST: api/v2/incidentes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IncidenteDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

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

            return CreatedAtAction(nameof(GetById), new { id = incidente.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "2.0" }, incidente);
        }

        // PUT: api/v2/incidentes/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] IncidenteDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var incidente = await _context.Incidentes.FindAsync(id);
            if (incidente == null) return NotFound();

            incidente.Tipo = dto.Tipo;
            incidente.Descricao = dto.Descricao;
            incidente.Data = dto.Data;
            incidente.AgenteId = dto.AgenteId;

            _context.Entry(incidente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/v2/incidentes/{id}
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
