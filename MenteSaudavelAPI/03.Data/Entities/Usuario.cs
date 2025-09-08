using MenteSaudavelAPI._03.Data.ValueObjects;
using MenteSaudavelAPI._04.Infrastructure.Dto;

namespace MenteSaudavelAPI._03.Data.Entities
{
    public class Usuario : Entity
    {
        #region PROPRIEDADES
        private string _nome;
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nome não pode ser vazio ou nulo.", nameof(value));
                }

                _nome = value;
            }
        }

        public Email Email { get; set; }
        public string EmailEndereco => Email.Endereco;

        private string _senha;
        public string Senha
        {
            get
            {
                return _senha;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Senha não pode ser vazia ou nula.", nameof(value));
                }

                _senha = value;
            }
        }

        private DateOnly _dataNascimento;
        public DateOnly DataNascimento
        {
            get
            {
                return _dataNascimento;
            }
            set
            {
                if (value > DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new ArgumentException("Data de nascimento não pode ser uma data futura.", nameof(value));
                }

                _dataNascimento = value;
            }
        }

        public Genero Genero { get; set; }

        public List<Questionario> Questionarios { get; private set; } = new List<Questionario>();
        #endregion

        #region CONSTRUTORES
        internal Usuario() { }

        public Usuario(UsuarioTO usuarioTO)
        {
            Nome = usuarioTO.Nome;
            Email = usuarioTO.Email;
            Senha = usuarioTO.Senha;
            DataNascimento = usuarioTO.DataNascimento;
            Genero = usuarioTO.Genero;
        }
        #endregion

        #region METODOS
        public void AdicionarQuestionario(Questionario questionario)
        {
            Questionarios.Add(questionario);
        }

        public UsuarioTO ToDto()
        {
            return new UsuarioTO()
            {
                UsuarioId = Id,
                Nome = Nome,
                Email = Email,
                DataNascimento = DataNascimento,
                Genero = Genero
            };
        }
        #endregion

        #region VALIDACOES

        #endregion
    }
}
