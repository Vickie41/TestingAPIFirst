using FirstTestingAPI.Models;

namespace FirstTestingAPI.Interfaces
{
    public interface IAuthService
    {
        LoginResponse GenerateToken(string username);
        bool ValidateCredentials(string username, string password);
    }
}