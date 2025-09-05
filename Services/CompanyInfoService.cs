using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstTestingAPI.Services
{
    public class CompanyInfoService : ICompanyInfoService
    {
        private readonly TaxClearSystemContext _context;
        private readonly ILogger<CompanyInfoService> _logger;

        public CompanyInfoService(TaxClearSystemContext context, ILogger<CompanyInfoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TbCompanyInfo?> GetCompanyInfoByRegistrationNo(string registrationNo)
        {
            try
            {
                return await _context.TbCompanyInfos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.RegistrationNo == registrationNo &&
                                             (!c.IsDeleted.HasValue || !c.IsDeleted.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting company by registration number: {RegistrationNo}", registrationNo);
                throw;
            }
        }

        public async Task<List<TbCompanyInfo>> GetAllCompanies()
        {
            try
            {
                return await _context.TbCompanyInfos
                    .AsNoTracking()
                    .Where(c => !c.IsDeleted.HasValue || !c.IsDeleted.Value)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all companies");
                throw;
            }
        }

        public async Task<TbCompanyInfo> CreateCompany(TbCompanyInfo companyInfo)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.TbCompanyInfos.Add(companyInfo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return companyInfo;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TbCompanyInfo> UpdateCompany(TbCompanyInfo companyInfo)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.TbCompanyInfos.Update(companyInfo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return companyInfo;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteCompany(string registrationNo)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var company = await GetCompanyInfoByRegistrationNo(registrationNo);
                if (company == null) return false;

                // Soft delete
                company.IsDeleted = true;
                company.ModifiedDate = DateTime.Now;

                _context.TbCompanyInfos.Update(company);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}