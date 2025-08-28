namespace FirstTestingAPI.Models
{
    public class CompanyInfoResponse
    {
        public string CompanyName { get; set; } = null!;
        public string? CompanyType { get; set; }
        public string? Address { get; set; }
        public string RegistrationNo { get; set; } = null!;

        // Constructor to map from TbCompanyInfo
        public CompanyInfoResponse(TbCompanyInfo companyInfo)
        {
            CompanyName = companyInfo.CompanyName;
            CompanyType = companyInfo.CompanyType;
            Address = companyInfo.Address;
            RegistrationNo = companyInfo.RegistrationNo;
        }
    }
}