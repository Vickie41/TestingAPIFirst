using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models.Entities;

public partial class VwLoanDetail
{
    public string NameEnglish { get; set; } = null!;

    public string? NameMm { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string FullNrc { get; set; } = null!;

    public string FatherNameEnglish { get; set; } = null!;

    public string? FatherNameMm { get; set; }

    public string Marital { get; set; } = null!;

    public string Race { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string OrganizationLoanId { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public string SeparateAccountNo { get; set; } = null!;

    public string ProductTypeCode { get; set; } = null!;

    public string ProductStatusCode { get; set; } = null!;

    public decimal DisbursedAmount { get; set; }

    public DateOnly DisbursedDate { get; set; }

    public decimal PrincipalOutstandingAmount { get; set; }

    public decimal PrincipalOverdueAmount { get; set; }

    public decimal InterestOutstandingAmount { get; set; }

    public decimal InterestOverdueAmount { get; set; }

    public decimal InterestRate { get; set; }

    public int Tenure { get; set; }

    public DateOnly ExpiredDate { get; set; }

    public DateTime? LastPaymentDate { get; set; }

    public DateTime LoanCreatedDate { get; set; }
}
