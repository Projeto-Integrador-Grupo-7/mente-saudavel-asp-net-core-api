using MenteSaudavelAPI._02.Services.Interfaces.Repositories;
using MenteSaudavelAPI._03.Data.Entities;
using MenteSaudavelAPI._04.Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;

namespace MenteSaudavelAPI._02.Services.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DataBaseContext context) : base(context)
        {
        }

        public Task<Usuario?> GetUsuarioByEmailESenha(UsuarioTO usuarioTO)
        {
            return Find(usuario =>
                usuario.Email.Endereco == usuarioTO.Email.Endereco &&
                usuario.Senha == usuarioTO.Senha)
                .SingleOrDefaultAsync();
        }
    }
}