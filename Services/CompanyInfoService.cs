using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstTestingAPI.Services
{
    public class CompanyInfoService : ICompanyInfoService
    {
        private readonly TaxClearSystemContext _context;

        public CompanyInfoService(TaxClearSystemContext context)
        {
            _context = context;
        }

        public async Task<TbCompanyInfo?> GetCompanyInfoByRegistrationNo(string registrationNo)
        {
            return await _context.TbCompanyInfos
                .FirstOrDefaultAsync(c => c.RegistrationNo == registrationNo);
        }

        public async Task<List<TbCompanyInfo>> GetAllCompanies()
        {
            return await _context.TbCompanyInfos
                .Where(c => !c.IsDeleted.HasValue || !c.IsDeleted.Value)
                .ToListAsync();
        }

        public async Task<TbCompanyInfo> CreateCompany(TbCompanyInfo companyInfo)
        {
            _context.TbCompanyInfos.Add(companyInfo);
            await _context.SaveChangesAsync();
            return companyInfo;
        }

        public async Task<TbCompanyInfo> UpdateCompany(TbCompanyInfo companyInfo)
        {
            _context.TbCompanyInfos.Update(companyInfo);
            await _context.SaveChangesAsync();
            return companyInfo;
        }

        public async Task<bool> DeleteCompany(string registrationNo)
        {
            var company = await GetCompanyInfoByRegistrationNo(registrationNo);
            if (company == null) return false;

            // Soft delete (recommended)
            company.IsDeleted = true;
            company.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}