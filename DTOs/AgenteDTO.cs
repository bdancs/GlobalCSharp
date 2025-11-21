using System.ComponentModel.DataAnnotations;

namespace SecurityState.API.DTOs
{
    public class AgenteDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Funcao { get; set; }
    }
}
