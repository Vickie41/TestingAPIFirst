namespace FirstTestingAPI.Models.Requests
{
    public class RepaymentRequest
    {
        public string MCISAccountNumber { get; set; } = string.Empty;
        public decimal RepaymentAmount { get; set; }
        public string RepaymentFor { get; set; } = string.Empty;
        public DateTime RepaymentDate { get; set; }
        public string OrganizationRepaymentId { get; set; } = string.Empty;
    }
}
