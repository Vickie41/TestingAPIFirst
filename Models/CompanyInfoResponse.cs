namespace FirstTestingAPI.Models
{
    public class CompanyInfoResponse
    {
        public string CompanyName { get; set; } = null!;
        public string RegistrationNo { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string? CompanyType { get; set; }
        public string? Address { get; set; }

        public CompanyInfoResponse(TbCompanyInfo companyInfo)
        {
            CompanyName = companyInfo.CompanyName;
            RegistrationNo = companyInfo.RegistrationNo;
            RegistrationDate = companyInfo.RegistrationDate;
            CompanyType = companyInfo.CompanyType;
            Address = companyInfo.Address;
        }
    }
}