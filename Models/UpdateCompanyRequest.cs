using System.ComponentModel.DataAnnotations;

namespace FirstTestingAPI.Models
{
    public class UpdateCompanyRequest
    {
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
        public string? CompanyName { get; set; }

        [StringLength(50, ErrorMessage = "Company type cannot exceed 50 characters")]
        public string? CompanyType { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }
    }
}