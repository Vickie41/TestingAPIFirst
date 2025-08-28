using FirstTestingAPI.Models;

namespace FirstTestingAPI.Interfaces
{
    public interface ICompanyInfoService
    {
        Task<TbCompanyInfo?> GetCompanyInfoByRegistrationNo(string registrationNo);
    }
}