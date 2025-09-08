using MenteSaudavelAPI._03.Data.ValueObjects;

namespace MenteSaudavelAPI._04.Infrastructure.Dto
{
    public class UsuarioTO
    {
        public Guid? UsuarioId { get; set; }

        public string Nome { get; set; }

        public Email Email { get; set; }

        public string Senha { get; set; }

        public DateOnly DataNascimento { get; set; }

        public Genero Genero { get; set; }
    }
}