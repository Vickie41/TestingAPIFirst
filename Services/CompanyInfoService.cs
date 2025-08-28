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
    }
}