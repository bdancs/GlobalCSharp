using System;

namespace SecurityState.API.Entities
{
    public class Incidente
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

        public int AgenteId { get; set; }
        public Agente Agente { get; set; }
    }
}

