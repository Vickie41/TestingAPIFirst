using System.ComponentModel.DataAnnotations;

namespace FirstTestingAPI.Models
{
    public class CreateCompanyRequest
    {
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
        public string CompanyName { get; set; } = null!;

        [Required(ErrorMessage = "Registration number is required")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Registration number must be exactly 9 digits")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Registration number must contain exactly 9 digits (0-9)")]
        public string RegistrationNo { get; set; } = null!;

        [Required(ErrorMessage = "Registration date is required")]
        public DateTime RegistrationDate { get; set; }

        [StringLength(50, ErrorMessage = "Company type cannot exceed 50 characters")]
        public string? CompanyType { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }
    }
}