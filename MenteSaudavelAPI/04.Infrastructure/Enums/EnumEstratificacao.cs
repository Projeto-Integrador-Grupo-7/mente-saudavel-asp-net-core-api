using System.ComponentModel.DataAnnotations;

namespace MenteSaudavelAPI._04.Infrastructure.Enums
{
    public enum EnumEstratificacao
    {
        [Display(Name = "Sofrimento Não Identificado")]
        NaoIdentificado = 0,

        [Display(Name = "Sofrimento Leve")]
        Leve = 1,

        [Display(Name = "Sofrimento Moderado")]
        Moderado = 2,

        [Display(Name = "Sofrimento Grave")]
        Grave = 3
    }
}
