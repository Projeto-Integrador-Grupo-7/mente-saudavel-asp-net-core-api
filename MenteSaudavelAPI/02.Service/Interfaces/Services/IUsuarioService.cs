using MenteSaudavelAPI._04.Infrastructure.Dto;

namespace MenteSaudavelAPI._02.Services.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioTO> ValidarLogin(UsuarioTO usuarioTO);

        Task<List<UsuarioTO>> GetUsuarios();

        Task<UsuarioTO> CriarUsuario(UsuarioTO usuarioTO);
    }
}