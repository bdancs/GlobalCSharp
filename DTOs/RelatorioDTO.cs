using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityState.API.DTOs
{
    public class RelatorioDTO
    {
        [Required]
        public string Conteudo { get; set; }

        [Required]
        public int IncidenteId { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
