using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models.Requests;
using FirstTestingAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstTestingAPI.Controllers
{
    [ApiController]
    [Route("api/v1/exchange")]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly ILogger<LoanController> _logger;

        public LoanController(ILoanService loanService, ILogger<LoanController> logger)
        {
            _loanService = loanService;
            _logger = logger;
        }


        [HttpGet("loan/healthcheck")]
        public IActionResult HealthCheck()
        {
            return Ok("Loan Service is running.");
        }

        [HttpPost("loan/create")]
      
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanRequest request)
        {
            try
            {
                var result = await _loanService.CreateLoanAsync(request);

                // Match the exact response format from API documentation
                var response = new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Successfully inserted customers and loan accounts. Total record : 1",
                    Data = new List<LoanCreateResponse> { result }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating loan");
                return StatusCode(500, new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        [HttpPost("loan/repayment")]
      
        public async Task<IActionResult> ProcessRepayment([FromBody] RepaymentRequest request)
        {
            try
            {
                var result = await _loanService.ProcessRepaymentAsync(request);

                // Match the exact response format from API documentation
                var response = new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Repayment process is successfully added.",
                    Data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing repayment");
                return StatusCode(500, new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        [HttpPost("report/search/loans/all")]
   
        public async Task<IActionResult> SearchLoans([FromBody] SearchLoansRequest request)
        {
            try
            {
                var result = await _loanService.SearchLoansAsync(request);

                // Match the exact response format from API documentation
                var response = new ApiResponse
                {
                    IsSuccess = true,
                    Data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching loans");
                return StatusCode(500, new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        [HttpGet("credit/search/history/nrc/{nrc}")]
       
        public async Task<IActionResult> GetCreditHistoryByNRC(string nrc)
        {
            try
            {
                var result = await _loanService.GetCreditHistoryByNRCAsync(nrc);

                // Match the exact response format from API documentation
                var response = new ApiResponse
                {
                    IsSuccess = true,
                    Data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting credit history for NRC: {NRC}", nrc);
                return StatusCode(500, new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }
    }
}