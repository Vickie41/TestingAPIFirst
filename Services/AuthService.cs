using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FirstTestingAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;

        public AuthService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public LoginResponse GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
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

        public bool ValidateCredentials(string username, string password)
        {
            // Replace this with your actual user validation logic
            var validUsers = new Dictionary<string, string>
            {
                { "admin", "Admin@123" },
                { "user", "User@123" },
                { "apiuser", "Apip@ssword" }
            };

            return validUsers.TryGetValue(username, out var validPassword) && validPassword == password;
        }
    }
}