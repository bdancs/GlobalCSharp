using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityState.API.DTOs
{
    public class IncidenteDTO
    {
        [Required]
        public string Tipo { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public int AgenteId { get; set; }
    }
}
