using MenteSaudavelAPI._03.Data.Entities;
using MenteSaudavelAPI._04.Infrastructure.Dto;

namespace MenteSaudavelAPI._02.Services.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetUsuarioByEmailESenha(UsuarioTO usuarioTO);
    }
}