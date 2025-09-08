namespace MenteSaudavelAPI._04.Infrastructure.Dto
{
    public class DashboardRequestTO
    {
        public Guid? UsuarioId { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? Genero { get; set; }
        public int? Idade { get; set; }
    }
}