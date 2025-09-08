using MenteSaudavelAPI._04.Infrastructure.Dto;

namespace MenteSaudavelAPI._03.Data.Entities
{
    public class Resposta : Entity
    {
        #region PROPRIEDADES
        public Questionario Questionario { get; private set; }
        public int Numero { get; private set; }
        public bool Valor { get; private set; }
        #endregion

        #region CONSTRUTORES
        internal Resposta() { }

        public Resposta(Questionario questionario, RespostaTO respostaTO)
        {
            Questionario = questionario;
            Numero = respostaTO.Numero;
            Valor = respostaTO.Valor;
        }
        #endregion

        #region METODOS
        public RespostaTO ToDto()
        {
            return new RespostaTO()
            {
                Id = Id,
                QuestionarioId = Questionario.Id,
                Numero = Numero,
                Valor = Valor
            };
        }
        #endregion
    }
}