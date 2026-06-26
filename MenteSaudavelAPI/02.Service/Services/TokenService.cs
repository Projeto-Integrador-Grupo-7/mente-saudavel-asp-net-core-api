using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MenteSaudavelAPI._02.Services.Interfaces.Services;
using MenteSaudavelAPI._03.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MenteSaudavelAPI._02.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            IConfigurationSection jwt = _configuration.GetSection("Jwt");
            string chave = jwt["Key"] ?? throw new InvalidOperationException("A chave JWT ('Jwt:Key') não foi configurada.");

            byte[] chaveBytes = Encoding.UTF8.GetBytes(chave);
            SigningCredentials credenciais = new SigningCredentials(
                new SymmetricSecurityKey(chaveBytes),
                SecurityAlgorithms.HmacSha256);

            // O usuarioId vai no claim padrão de identificador, de onde os endpoints
            // protegidos derivam o dono do recurso (em vez de confiar no corpo da requisição).
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.EmailEndereco)
            };

            int expiraEmMinutos = int.TryParse(jwt["ExpireMinutes"], out int minutos) ? minutos : 60;

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiraEmMinutos),
                signingCredentials: credenciais);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
