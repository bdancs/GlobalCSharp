namespace SecurityState.API.Entities
{
    public class Agente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Funcao { get; set; }

        public List<Incidente> Incidentes { get; set; }
    }
}
