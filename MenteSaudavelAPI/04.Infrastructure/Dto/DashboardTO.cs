using MenteSaudavelAPI._03.Data.ValueObjects;
using MenteSaudavelAPI._04.Infrastructure.Enums;
using MenteSaudavelAPI._04.Infrastructure.Extensions;

namespace MenteSaudavelAPI._04.Infrastructure.Dto
{
    public class DashboardTO
    {
        public Guid? UsuarioId { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public Genero? Genero { get; set; }
        public int? Idade { get; set; }


        public static explicit operator DashboardTO(DashboardRequestTO requestTO)
        {
            if (requestTO is null)
            {
                return new DashboardTO();
            }

            DashboardTO dashboardTO = new DashboardTO
            {
                UsuarioId = requestTO.UsuarioId,
                DataInicio = requestTO.DataInicio,
                DataFim = requestTO.DataFim,
                Idade = requestTO.Idade
            };

            dashboardTO.Genero = requestTO.Genero switch
            {
                "F" => new Genero(EnumGenero.Feminino.GetIntValue()),
                "M" => new Genero(EnumGenero.Masculino.GetIntValue()),
                "O" => new Genero(EnumGenero.Outro.GetIntValue()),
                _ => null
            };

            return dashboardTO;
        }
    }
}