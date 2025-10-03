using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models.Entities;

public partial class TbLoanMaster
{
    public int LoanId { get; set; }

    public string OrganizationLoanId { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public string SeparateAccountNo { get; set; } = null!;

    public string ApplicantTypeCode { get; set; } = null!;

    public string ProductTypeCode { get; set; } = null!;

    public string ProductStatusCode { get; set; } = null!;

    public decimal PrincipalAmount { get; set; }

    public decimal DisbursedAmount { get; set; }

    public DateOnly DisbursedDate { get; set; }

    public DateOnly ExpiredDate { get; set; }

    public decimal InterestRate { get; set; }

    public decimal PrincipalInstalmentAmount { get; set; }

    public string PrincipalPaymentFrequency { get; set; } = null!;

    public decimal InterestInstalmentAmount { get; set; }

    public string InterestPaymentFrequency { get; set; } = null!;

    public decimal PrincipalOverdueAmount { get; set; }

    public decimal InterestOverdueAmount { get; set; }

    public decimal PrincipalOutstandingAmount { get; set; }

    public decimal InterestOutstandingAmount { get; set; }

    public int Tenure { get; set; }

    public string AccountTypeCode { get; set; } = null!;

    public bool SmeFlag { get; set; }

    public string? IndustrialSectorCode { get; set; }

    public decimal? LastPaymentAmount { get; set; }

    public DateTime? LastPaymentDate { get; set; }

    public string? LastPaymentFor { get; set; }

    public int PersonPkid { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TbPersonalInformation PersonPk { get; set; } = null!;

    public virtual ICollection<TbLoanCollateral> TbLoanCollaterals { get; set; } = new List<TbLoanCollateral>();

    public virtual ICollection<TbLoanReturnTransactionDetail> TbLoanReturnTransactionDetails { get; set; } = new List<TbLoanReturnTransactionDetail>();
}
