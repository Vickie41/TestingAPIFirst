using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models.Requests;
using FirstTestingAPI.Models.Responses; // Make sure this namespace exists
using Microsoft.IdentityModel.Tokens;

namespace FirstTestingAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest)
        {
            if (!ValidateCredentials(loginRequest.Username, loginRequest.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            return await Task.FromResult(GenerateJwtToken(loginRequest.Username));
        }

        private LoginResponse GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LoginResponse
            {
                Token = tokenString,
                Expiry = token.ValidTo,
                Message = "Authentication successful",
                StatusCode = 200
            };
        }

        private bool ValidateCredentials(string username, string password)
        {
            // Replace with your real authentication logic or DB validation
            var validUsers = new Dictionary<string, string>
            {
                { "admin", "password" },
                { "admin_1", "Admin@123" },
                { "user", "User@123" },
                { "apiuser", "Apip@ssword" }
            };

            return validUsers.TryGetValue(username, out var validPassword) && validPassword == password;
        }
    }
}
