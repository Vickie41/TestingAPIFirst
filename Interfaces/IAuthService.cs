using FirstTestingAPI.Models.Requests;

namespace FirstTestingAPI.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(LoginRequest loginRequest);
    }
}