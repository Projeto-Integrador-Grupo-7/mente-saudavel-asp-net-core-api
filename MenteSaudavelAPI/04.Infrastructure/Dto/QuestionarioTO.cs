using MenteSaudavelAPI._03.Data.ValueObjects;

namespace MenteSaudavelAPI._04.Infrastructure.Dto
{
    public class QuestionarioTO
    {
        public Guid? Id { get; set; }

        public Guid RespondenteId { get; set; }
        public string? RespondenteNome { get; set; }

        public int? Pontuacao { get; set; }

        public Estratificacao? Estratificacao { get; set; }

        public string? DataEnvio { get; set; }

        public List<RespostaTO> ListaRespostas { get; set; }
    }
}