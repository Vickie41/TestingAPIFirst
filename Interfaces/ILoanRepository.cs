using FirstTestingAPI.Models.Requests;
using FirstTestingAPI.Models.Responses;

namespace FirstTestingAPI.Repositories
{
    public interface ILoanRepository
    {
        Task<LoanCreateResponse> CreateLoanAsync(CreateLoanRequest request);
        Task<List<LoanDetailsResponse>> SearchLoansAsync(SearchLoansRequest request);
        Task<RepaymentResponse> ProcessRepaymentAsync(RepaymentRequest request);
        Task<LoanDetailsResponse> GetCreditHistoryByNRCAsync(string nrc);
    }
}
