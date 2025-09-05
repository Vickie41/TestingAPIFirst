using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data", 400));
                }

                if (_authService.ValidateCredentials(request.Username, request.Password))
                {
                    var response = _authService.GenerateToken(request.Username);
                    _logger.LogInformation("User {Username} logged in successfully", request.Username);
                    return Ok(ApiResponse<LoginResponse>.SuccessResponse(response, "Login successful"));
                }

                _logger.LogWarning("Failed login attempt for username: {Username}", request.Username);
                return Unauthorized(ApiResponse<object>.ErrorResponse("Invalid username or password", 401));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for username: {Username}", request.Username);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("Internal server error", 500));
            }
        }

        [HttpPost("validate")]
        public IActionResult ValidateToken([FromHeader] string authorization)
        {
            try
            {
                if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Bearer "))
                {
                    return Unauthorized(ApiResponse<object>.ErrorResponse("Invalid token format", 401));
                }

                var token = authorization.Substring("Bearer ".Length).Trim();
                return Ok(ApiResponse<object>.SuccessResponse(null, "Token is valid"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating token");
                return Unauthorized(ApiResponse<object>.ErrorResponse("Invalid token", 401));
            }
        }
    }
}