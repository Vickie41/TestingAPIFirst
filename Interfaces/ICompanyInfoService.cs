using FirstTestingAPI.Models;

namespace FirstTestingAPI.Interfaces
{
    public interface ICompanyInfoService
    {
        Task<TbCompanyInfo?> GetCompanyInfoByRegistrationNo(string registrationNo);
        Task<List<TbCompanyInfo>> GetAllCompanies();
        Task<TbCompanyInfo> CreateCompany(TbCompanyInfo companyInfo);
        Task<TbCompanyInfo> UpdateCompany(TbCompanyInfo companyInfo);
        Task<bool> DeleteCompany(string registrationNo);
    }
}