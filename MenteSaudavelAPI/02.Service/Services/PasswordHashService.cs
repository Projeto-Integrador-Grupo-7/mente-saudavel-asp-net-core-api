using MenteSaudavelAPI._02.Services.Interfaces.Services;
using BC = BCrypt.Net.BCrypt;

namespace MenteSaudavelAPI._02.Services.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword(string password)
        {
            return BC.HashPassword(password, workFactor: 12);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BC.Verify(password, hash);
        }
    }
}
