using System.ComponentModel.DataAnnotations;

namespace FirstTestingAPI.Models
{
    public class UpdateCompanyRequest
    {
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Company name must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s\.,&'-]+$",
            ErrorMessage = "Company name can only contain letters, numbers, spaces, and basic punctuation (.,&'-)")]
        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Company type is required")]
        [StringLength(50, ErrorMessage = "Company type cannot exceed 50 characters")]
        [RegularExpression(@"^(LLC|Ltd|Inc|PLC|Partnership|Sole Proprietorship|Nonprofit)$",
            ErrorMessage = "Company type must be one of: LLC, Ltd, Inc, PLC, Partnership, Sole Proprietorship, Nonprofit")]
        public string? CompanyType { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.-/#]+$",
            ErrorMessage = "Address can only contain letters, numbers, spaces, and ,.-/# symbols")]
        public string? Address { get; set; }
    }
}