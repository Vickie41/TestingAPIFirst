using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models.Requests;
using FirstTestingAPI.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FirstTestingAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var token = await _authService.AuthenticateAsync(loginRequest);


                return Ok(new ApiResponse<LoginResponse>
                {
                    IsSuccess = true,
                    Message = "Login successful",
                    Data = new LoginResponse
                    {
                        Token = token,
                        Expiry = DateTime.UtcNow.AddMinutes(30), // Match your JWT expiry
                        Message = "Authentication successful",
                        StatusCode = 200
                    }
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Login failed for user: {Username}", loginRequest.Username);
                return Unauthorized(new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Invalid credentials",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Internal server error",
                    Data = null
                });
            }
        }
    }
}