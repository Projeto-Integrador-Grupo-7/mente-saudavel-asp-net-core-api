namespace MenteSaudavelAPI._02.Services.Interfaces.Services
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}
