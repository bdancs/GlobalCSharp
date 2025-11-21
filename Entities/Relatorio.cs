using System;

namespace SecurityState.API.Entities
{
    public class Relatorio
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public DateTime CriadoEm { get; set; }

        public int IncidenteId { get; set; }
        public Incidente Incidente { get; set; }
    }
}
