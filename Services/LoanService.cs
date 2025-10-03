using FirstTestingAPI.Data;
using FirstTestingAPI.Interfaces;
using FirstTestingAPI.Services;
using FirstTestingAPI.Models.Requests;
using FirstTestingAPI.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstTestingAPI.Repositories;

namespace FirstTestingAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ILogger<LoanService> _logger;

        public LoanService(ILoanRepository loanRepository, ILogger<LoanService> logger)
        {
            _loanRepository = loanRepository;
            _logger = logger;
        }

        public async Task<LoanCreateResponse> CreateLoanAsync(CreateLoanRequest request)
        {
            try
            {
                var result = await _loanRepository.CreateLoanAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating loan");
                throw;
            }
        }

        public async Task<RepaymentResponse> ProcessRepaymentAsync(RepaymentRequest request)
        {
            try
            {
                var result = await _loanRepository.ProcessRepaymentAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing repayment");
                throw;
            }
        }

        public async Task<List<LoanDetailsResponse>> SearchLoansAsync(SearchLoansRequest request)
        {
            try
            {
                var result = await _loanRepository.SearchLoansAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching loans");
                throw;
            }
        }

        public async Task<LoanDetailsResponse> GetCreditHistoryByNRCAsync(string nrc)
        {
            try
            {
                var result = await _loanRepository.GetCreditHistoryByNRCAsync(nrc);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting credit history for NRC: {NRC}", nrc);
                throw;
            }
        }
    }
}