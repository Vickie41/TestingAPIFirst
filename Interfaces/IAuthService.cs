using FirstTestingAPI.Models.Requests;
using FirstTestingAPI.Models.Responses;

namespace FirstTestingAPI.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest);
    }
}
