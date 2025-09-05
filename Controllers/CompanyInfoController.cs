using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FirstTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompanyByRegistrationNo([FromQuery] string registrationNo)
        {
            try
            {
                // ✅ Check if missing/empty
                if (string.IsNullOrEmpty(registrationNo))
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse(
                        "Registration number is required",
                        StatusCodes.Status400BadRequest,
                        new List<string> { "Registration number parameter is missing or empty" }
                    ));
                }

                // ✅ Check format: must be exactly 9 digits
                if (!System.Text.RegularExpressions.Regex.IsMatch(registrationNo, @"^\d{9}$"))
                {
                    return UnprocessableEntity(ApiResponse<object>.ErrorResponse(
                        "Invalid registration number format",
                        StatusCodes.Status422UnprocessableEntity,
                        new List<string> { "Registration number must be exactly 9 digits (0-9)" }
                    ));
                }

                // ✅ Lookup company info
                var companyInfo = await _companyInfoService.GetCompanyInfoByRegistrationNo(registrationNo);

                if (companyInfo == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse(
                        $"Company with registration number '{registrationNo}' not found",
                        StatusCodes.Status404NotFound
                    ));
                }

                // ✅ Success response
                var response = new CompanyInfoResponse(companyInfo);
                return Ok(ApiResponse<CompanyInfoResponse>.SuccessResponse(
                    response,
                    "Company found successfully",
                    StatusCodes.Status200OK
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching company by registration number: {RegistrationNo}", registrationNo);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.ErrorResponse(
                        "An internal server error occurred while processing your request",
                        StatusCodes.Status500InternalServerError
                    )
                );
            }
        }


        // GET: api/CompanyInfo
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies = await _companyInfoService.GetAllCompanies();

                if (companies == null || companies.Count == 0)
                {
                    return NoContent(); // 204 No Content
                }

                var response = companies.Select(c => new CompanyInfoResponse(c)).ToList();
                return Ok(ApiResponse<List<CompanyInfoResponse>>.SuccessResponse(
                    response,
                    "Companies retrieved successfully",
                    StatusCodes.Status200OK
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all companies");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.ErrorResponse(
                        "An internal server error occurred while fetching companies",
                        StatusCodes.Status500InternalServerError
                    )
                );
            }
        }

        // POST: api/CompanyInfo
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(ApiResponse<object>.ValidationErrorResponse(errors));
                }

                var existingCompany = await _companyInfoService.GetCompanyInfoByRegistrationNo(request.RegistrationNo);
                if (existingCompany != null)
                {
                    return Conflict(ApiResponse<object>.ErrorResponse(
                        $"Company with registration number '{request.RegistrationNo}' already exists",
                        StatusCodes.Status409Conflict
                    ));
                }

                var newCompany = new TbCompanyInfo
                {
                    CompanyName = request.CompanyName,
                    RegistrationNo = request.RegistrationNo,
                    RegistrationDate = request.RegistrationDate,
                    CompanyType = request.CompanyType,
                    Address = request.Address,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                var createdCompany = await _companyInfoService.CreateCompany(newCompany);

                if (createdCompany == null)
                {
                    return StatusCode(
                        StatusCodes.Status422UnprocessableEntity,
                        ApiResponse<object>.ErrorResponse(
                            "Failed to create company due to database constraints",
                            StatusCodes.Status422UnprocessableEntity
                        )
                    );
                }

                var response = new CompanyInfoResponse(createdCompany);
                return CreatedAtAction(
                    nameof(GetCompanyByRegistrationNo),
                    new { registrationNo = response.RegistrationNo },
                    ApiResponse<CompanyInfoResponse>.SuccessResponse(
                        response,
                        "Company created successfully",
                        StatusCodes.Status201Created
                    )
                );
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while creating company");
                return StatusCode(
                    StatusCodes.Status422UnprocessableEntity,
                    ApiResponse<object>.ErrorResponse(
                        "Database constraint violation. Please check your data.",
                        StatusCodes.Status422UnprocessableEntity
                    )
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.ErrorResponse(
                        "An internal server error occurred while creating the company",
                        StatusCodes.Status500InternalServerError
                    )
                );
            }
        }

        // PUT: api/CompanyInfo/{registrationNo}
        [HttpPut("{registrationNo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCompany(string registrationNo, [FromBody] CreateCompanyRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(ApiResponse<object>.ValidationErrorResponse(errors));
                }

                var existingCompany = await _companyInfoService.GetCompanyInfoByRegistrationNo(registrationNo);
                if (existingCompany == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse(
                        $"Company with registration number '{registrationNo}' not found",
                        StatusCodes.Status404NotFound
                    ));
                }

                // Update the existing company
                existingCompany.CompanyName = request.CompanyName;
                existingCompany.RegistrationDate = request.RegistrationDate;
                existingCompany.CompanyType = request.CompanyType;
                existingCompany.Address = request.Address;
                existingCompany.ModifiedDate = DateTime.Now;

                var updatedCompany = await _companyInfoService.UpdateCompany(existingCompany);

                if (updatedCompany == null)
                {
                    return StatusCode(
                        StatusCodes.Status422UnprocessableEntity,
                        ApiResponse<object>.ErrorResponse(
                            "Failed to update company due to database constraints",
                            StatusCodes.Status422UnprocessableEntity
                        )
                    );
                }

                var response = new CompanyInfoResponse(updatedCompany);
                return Ok(ApiResponse<CompanyInfoResponse>.SuccessResponse(
                    response,
                    "Company updated successfully",
                    StatusCodes.Status200OK
                ));
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while updating company: {RegistrationNo}", registrationNo);
                return StatusCode(
                    StatusCodes.Status422UnprocessableEntity,
                    ApiResponse<object>.ErrorResponse(
                        "Database constraint violation. Please check your data.",
                        StatusCodes.Status422UnprocessableEntity
                    )
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company: {RegistrationNo}", registrationNo);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.ErrorResponse(
                        "An internal server error occurred while updating the company",
                        StatusCodes.Status500InternalServerError
                    )
                );
            }
        }

        // DELETE: api/CompanyInfo/{registrationNo}
        [HttpDelete("{registrationNo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCompany(string registrationNo)
        {
            try
            {
                var company = await _companyInfoService.GetCompanyInfoByRegistrationNo(registrationNo);
                if (company == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse(
                        $"Company with registration number '{registrationNo}' not found",
                        StatusCodes.Status404NotFound
                    ));
                }

                // Check if company can be deleted (business rule example)
                if (company.CreatedDate.HasValue &&
                    (DateTime.Now - company.CreatedDate.Value).TotalDays < 1)
                {
                    return StatusCode(
                        StatusCodes.Status403Forbidden,
                        ApiResponse<object>.ErrorResponse(
                            "Cannot delete company created within the last 24 hours",
                            StatusCodes.Status403Forbidden
                        )
                    );
                }

                var result = await _companyInfoService.DeleteCompany(registrationNo);

                if (result)
                {
                    return Ok(ApiResponse<object>.SuccessResponse(
                        null,
                        $"Company '{registrationNo}' deleted successfully",
                        StatusCodes.Status200OK
                    ));
                }
                else
                {
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        ApiResponse<object>.ErrorResponse(
                            "Failed to delete the company due to an internal error",
                            StatusCodes.Status500InternalServerError
                        )
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting company: {RegistrationNo}", registrationNo);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.ErrorResponse(
                        "An internal server error occurred while deleting the company",
                        StatusCodes.Status500InternalServerError
                    )
                );
            }
        }
    }
}