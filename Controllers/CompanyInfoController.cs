using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyInfoController : ControllerBase
    {
        private readonly ICompanyInfoService _companyInfoService;

        public CompanyInfoController(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        // GET: api/CompanyInfo/GetCompanyByRegistrationNo
        [HttpGet("GetCompanyByRegistrationNo")]
        public async Task<IActionResult> GetCompanyByRegistrationNo([FromQuery] string registrationNo)
        {
            try
            {
                if (string.IsNullOrEmpty(registrationNo))
                {
                    return BadRequest("Registration number is required.");
                }

                var companyInfo = await _companyInfoService.GetCompanyInfoByRegistrationNo(registrationNo);

                if (companyInfo == null)
                {
                    return NotFound($"Company with registration number '{registrationNo}' not found.");
                }

                // Map to response model (only include required fields)
                var response = new CompanyInfoResponse(companyInfo);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching company information for RegistrationNo: {RegistrationNo}", registrationNo);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}