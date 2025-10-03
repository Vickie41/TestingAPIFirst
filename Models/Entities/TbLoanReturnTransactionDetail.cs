using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models.Entities;

public partial class TbLoanReturnTransactionDetail
{
    public int TransactionId { get; set; }

    public string McisaccountNumber { get; set; } = null!;

    public decimal RepaymentAmount { get; set; }

    public string RepaymentFor { get; set; } = null!;

    public DateOnly RepaymentDate { get; set; }

    public string OrganizationRepaymentId { get; set; } = null!;

    public string McisrepaymentNumber { get; set; } = null!;

    public int LoanId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual TbLoanMaster Loan { get; set; } = null!;
}
