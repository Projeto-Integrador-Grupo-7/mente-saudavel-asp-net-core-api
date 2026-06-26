using MenteSaudavelAPI._02.Services.Interfaces.Services;
using MenteSaudavelAPI._03.Data.Entities;
using MenteSaudavelAPI._04.Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;

namespace MenteSaudavelAPI._02.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUnitOfWork unitOfWork, IPasswordHashService passwordHashService, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHashService = passwordHashService;
            _tokenService = tokenService;
        }

        public async Task<LoginRespostaTO> ValidarLogin(UsuarioTO usuarioTO)
        {
            Usuario? usuario = await _unitOfWork.UsuarioRepository.GetUsuarioByEmail(usuarioTO);

            if (usuario is null || !_passwordHashService.VerifyPassword(usuarioTO.Senha, usuario.SenhaHash))
            {
                throw new ArgumentException("Email ou senha incorretos.");
            }

            return new LoginRespostaTO
            {
                Token = _tokenService.GerarToken(usuario),
                Usuario = usuario.ToDto()
            };
        }

        public async Task<List<UsuarioTO>> GetUsuarios()
        {
            List<Usuario> listaUsuarios = await _unitOfWork.UsuarioRepository.GetAll().ToListAsync();

            List<UsuarioTO> listaUsuariosTO = listaUsuarios.Select(usuario => usuario.ToDto()).ToList();

            return listaUsuariosTO;
        }

        public async Task<UsuarioTO> CriarUsuario(UsuarioTO usuarioTO)
        {
            Usuario usuario = new Usuario(usuarioTO);
            
            string senhaHash = _passwordHashService.HashPassword(usuarioTO.Senha);
            usuario.DefinirSenhaHash(senhaHash);

            _unitOfWork.UsuarioRepository.Add(usuario);
            await _unitOfWork.SaveChangesAsync();

            // Retorna um DTO derivado da entidade (ToDto não inclui senha/hash),
            // evitando devolver a senha em claro recebida na requisição ao cliente.
            return usuario.ToDto();
        }
    }
}