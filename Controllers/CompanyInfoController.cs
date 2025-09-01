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
        private readonly ILogger<CompanyInfoController> _logger;

        public CompanyInfoController(ICompanyInfoService companyInfoService, ILogger<CompanyInfoController> logger)
        {
            _companyInfoService = companyInfoService;
            _logger = logger;
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

                var response = new CompanyInfoResponse(companyInfo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching company by registration number: {RegistrationNo}", registrationNo);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET: api/CompanyInfo
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies = await _companyInfoService.GetAllCompanies();
                var response = companies.Select(c => new CompanyInfoResponse(c)).ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all companies");
                return StatusCode(500, "An error occurred while fetching companies.");
            }
        }

        // POST: api/CompanyInfo
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingCompany = await _companyInfoService.GetCompanyInfoByRegistrationNo(request.RegistrationNo);
                if (existingCompany != null)
                {
                    return Conflict($"Company with registration number '{request.RegistrationNo}' already exists.");
                }

                var newCompany = new TbCompanyInfo
                {
                    CompanyName = request.CompanyName,
                    RegistrationNo = request.RegistrationNo,
                    RegistrationDate = request.RegistrationDate, // Add this line
                    CompanyType = request.CompanyType,
                    Address = request.Address,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                    // Other fields will be null/default values
                };

                var createdCompany = await _companyInfoService.CreateCompany(newCompany);
                var response = new CompanyInfoResponse(createdCompany);

                return CreatedAtAction(nameof(GetCompanyByRegistrationNo),
                    new { registrationNo = response.RegistrationNo }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company");
                return StatusCode(500, $"An error occurred while creating the company. Details: {ex.Message}");
            }
        }

        // PUT: api/CompanyInfo/{registrationNo}
        [HttpPut("{registrationNo}")]
        public async Task<IActionResult> UpdateCompany(string registrationNo, [FromBody] UpdateCompanyRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingCompany = await _companyInfoService.GetCompanyInfoByRegistrationNo(registrationNo);
                if (existingCompany == null)
                {
                    return NotFound($"Company with registration number '{registrationNo}' not found.");
                }

                // Update only the allowed fields
                existingCompany.CompanyName = request.CompanyName ?? existingCompany.CompanyName;
                existingCompany.CompanyType = request.CompanyType ?? existingCompany.CompanyType;
                existingCompany.Address = request.Address ?? existingCompany.Address;
                existingCompany.ModifiedDate = DateTime.Now;

                var updatedCompany = await _companyInfoService.UpdateCompany(existingCompany);
                var response = new CompanyInfoResponse(updatedCompany);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company: {RegistrationNo}", registrationNo);
                return StatusCode(500, "An error occurred while updating the company.");
            }
        }

        // DELETE: api/CompanyInfo/{registrationNo}
        [HttpDelete("{registrationNo}")]
        public async Task<IActionResult> DeleteCompany(string registrationNo)
        {
            try
            {
                var company = await _companyInfoService.GetCompanyInfoByRegistrationNo(registrationNo);
                if (company == null)
                {
                    return NotFound($"Company with registration number '{registrationNo}' not found.");
                }

                var result = await _companyInfoService.DeleteCompany(registrationNo);

                if (result)
                {
                    return Ok(new { message = $"Company '{registrationNo}' deleted successfully." });
                }
                else
                {
                    return StatusCode(500, "Failed to delete the company.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting company: {RegistrationNo}", registrationNo);
                return StatusCode(500, "An error occurred while deleting the company.");
            }
        }
    }
}