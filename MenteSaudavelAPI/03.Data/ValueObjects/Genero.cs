using MenteSaudavelAPI._04.Infrastructure.Enums;
using MenteSaudavelAPI._04.Infrastructure.Extensions;

namespace MenteSaudavelAPI._03.Data.ValueObjects
{
    public class Genero
    {
        public int Valor { get; }
        public string Descricao { get; }

        internal Genero() { }

        public Genero(int valor)
        {
            Valor = valor;

            Descricao = Valor switch
            {
                1 => EnumGenero.Feminino.GetDisplayName(),
                2 => EnumGenero.Masculino.GetDisplayName(),
                3 => EnumGenero.Outro.GetDisplayName(),
                _ => throw new ArgumentOutOfRangeException(nameof(valor), "Valor inválido para gênero.")
            };
        }

        public Genero(char genero) : this(
            genero switch
            {
                'F' => EnumGenero.Feminino.GetIntValue(),
                'M' => EnumGenero.Masculino.GetIntValue(),
                'O' => EnumGenero.Outro.GetIntValue(),
                _ => throw new ArgumentOutOfRangeException(nameof(genero), "Gênero inválido.")
            }
        ) { }

        public bool IsFeminino => Valor == EnumGenero.Feminino.GetIntValue();
        public bool IsMasculino => Valor == EnumGenero.Masculino.GetIntValue();
        public bool IsOutro => Valor == EnumGenero.Outro.GetIntValue();
    }
}
