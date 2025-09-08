using MenteSaudavelAPI._04.Infrastructure.Enums;
using MenteSaudavelAPI._04.Infrastructure.Extensions;

namespace MenteSaudavelAPI._03.Data.ValueObjects
{
    public class Estratificacao
    {
        public int Valor { get; }

        public string Descricao => Valor switch
        {
            0 => EnumEstratificacao.NaoIdentificado.GetDisplayName(),
            1 => EnumEstratificacao.Leve.GetDisplayName(),
            2 => EnumEstratificacao.Moderado.GetDisplayName(),
            3 => EnumEstratificacao.Grave.GetDisplayName(),
            _ => throw new ArgumentOutOfRangeException("Valor inválido.")
        };

        internal Estratificacao() { }

        public Estratificacao(int pontuacao)
        {
            Valor = pontuacao switch
            {
                0 => EnumEstratificacao.NaoIdentificado.GetIntValue(),
                > 0 and <= 7 => EnumEstratificacao.Leve.GetIntValue(),
                >= 8 and <= 14 => EnumEstratificacao.Moderado.GetIntValue(),
                >= 15 and <= 20 => EnumEstratificacao.Grave.GetIntValue(),
                _ => throw new ArgumentOutOfRangeException(nameof(pontuacao), "Pontuação inválida.")
            };
        }

        public override string ToString() => Descricao;

        public bool IsNaoIdentificado => Valor == EnumEstratificacao.NaoIdentificado.GetIntValue();
        public bool IsLeve => Valor == EnumEstratificacao.Leve.GetIntValue();
        public bool IsModerado => Valor == EnumEstratificacao.Moderado.GetIntValue();
        public bool IsGrave => Valor == EnumEstratificacao.Grave.GetIntValue();
    }
}