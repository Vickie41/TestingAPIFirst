namespace FirstTestingAPI.Models.Responses
{
    public class LoanCreateResponse
    {
        public string Message { get; set; } = string.Empty;
        public string CustomerNameEnglish { get; set; } = string.Empty;
        public string? CustomerNameMyanmar { get; set; }
        public string NRCNumber { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public string FatherNameEnglish { get; set; } = string.Empty;
        public string? FatherNameMyanmar { get; set; }
        public string? AccountNumber { get; set; }
        public string MCISAccountNumber { get; set; } = string.Empty;
        public string OrganizationLoanID { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
    }
}