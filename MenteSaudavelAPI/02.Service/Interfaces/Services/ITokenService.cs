using MenteSaudavelAPI._03.Data.Entities;

namespace MenteSaudavelAPI._02.Services.Interfaces.Services
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
