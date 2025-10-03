using FirstTestingAPI.Models.Requests;
using FirstTestingAPI.Models.Responses;
using System.Threading.Tasks;

namespace FirstTestingAPI.Interfaces
{
    public interface ILoanService
    {
        Task<LoanCreateResponse> CreateLoanAsync(CreateLoanRequest request);
        Task<RepaymentResponse> ProcessRepaymentAsync(RepaymentRequest request);
        Task<List<LoanDetailsResponse>> SearchLoansAsync(SearchLoansRequest request);
        Task<LoanDetailsResponse> GetCreditHistoryByNRCAsync(string nrc);
    }
}