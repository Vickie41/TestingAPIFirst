using FirstTestingAPI.Models.Requests;
using System.ComponentModel.DataAnnotations;

namespace FirstTestingAPI.Models.Requests
{
    public class CreateLoanRequest
    {
        public CustomerInformation CustomerInformation { get; set; } = new CustomerInformation();
        public LoanAccount LoanAccount { get; set; } = new LoanAccount();
        public LoanCollateral LoanCollateral { get; set; } = new LoanCollateral();
    }
}


