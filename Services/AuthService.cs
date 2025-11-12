using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models.Requests;
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

        public async Task<string> AuthenticateAsync(LoginRequest loginRequest)
        {
            
            if (loginRequest.Username == "admin" && loginRequest.Password == "password")
            {
                return await Task.FromResult(GenerateJwtToken(loginRequest.Username));
            }

            throw new UnauthorizedAccessException("Invalid credentials");
        }

        private string GenerateJwtToken(string username)
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
<<<<<<< HEAD
            return tokenHandler.WriteToken(token);
=======
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
                { "admin_1", "Admin@123" },
                { "user", "User@123" },
                { "apiuser", "Apip@ssword" }
            };

            return validUsers.TryGetValue(username, out var validPassword) && validPassword == password;
>>>>>>> 49bafc2 (Remove obj folder and update .gitignore)
        }
    }
}