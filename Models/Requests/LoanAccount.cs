using System.ComponentModel.DataAnnotations;

namespace FirstTestingAPI.Models.Requests
{
    public class LoanAccount
    {
        public string OrganizationLoanID { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string SeparateAccountNo { get; set; } = string.Empty;
        public string ApplicantTypeCode { get; set; } = string.Empty;
        public string ProductTypeCode { get; set; } = string.Empty;
        public string ProductStatusCode { get; set; } = string.Empty;
        public decimal PrincipalAmount { get; set; } // Fixed spelling
        public decimal DisbursedAmount { get; set; } // Fixed spelling
        public DateTime DisbursedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public decimal InterestRate { get; set; }
        public decimal PrincipalInstalmentAmount { get; set; } // Fixed spelling
        public string PrincipalPaymentFrequency { get; set; } = string.Empty;
        public decimal InterestInstalmentAmount { get; set; } // Fixed spelling and removed underscore
        public string InterestPaymentFrequency { get; set; } = string.Empty;
        public decimal PrincipalOverdueAmount { get; set; }
        public decimal InterestOverdueAmount { get; set; }
        public decimal PrincipalOutstandingAmount { get; set; }
        public decimal InterestOutstandingAmount { get; set; }
        public int Tenure { get; set; }
        public string AccountTypeCode { get; set; } = string.Empty;
        public bool SME_Flag { get; set; }
        public string? IndustrialSectorCode { get; set; }
        public decimal? LastPaymentAmount { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public string? LastPaymentFor { get; set; }
    }
}
