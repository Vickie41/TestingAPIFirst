namespace FirstTestingAPI.Models.Responses
{
    public class LoanDetailsResponse
    {
        public CustomerDetails CustomerDetails { get; set; } = new CustomerDetails();
        public List<AccountDetail> AccountDetails { get; set; } = new List<AccountDetail>();
    }

    public class CustomerDetails
    {
        public string NameEnglish { get; set; } = string.Empty;
        public string? NameMyanmar { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string NRCNumber { get; set; } = string.Empty;
        public string FatherNameEnglish { get; set; } = string.Empty;
        public string? FatherNameMyanmar { get; set; }
        public string Marital { get; set; } = string.Empty;
        public string Race { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<string> Address { get; set; } = new List<string>();
    }

    public class AccountDetail
    {
        public string Creditor { get; set; } = string.Empty;
        public string LoanID { get; set; } = string.Empty;
        public string? AccountNumber { get; set; }
        public string MCISAccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string ProductStatus { get; set; } = string.Empty;
        public string AccountStatus { get; set; } = string.Empty;
        public decimal DisbursedAmount { get; set; }
        public DateTime DisbursedDate { get; set; }
        public decimal PrincipalOutstandingAmount { get; set; }
        public decimal PrincipalOverdueAmount { get; set; }
        public decimal InterestOutstandingAmount { get; set; }
        public decimal InterestOverdueAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int Tenure { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<Collateral> Collaterals { get; set; } = new List<Collateral>();
    }

    public class Collateral
    {
        public string Type { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MarketValue { get; set; }
        public decimal ForceSaleValue { get; set; }
        public string FullAddress { get; set; } = string.Empty;
    }
}
