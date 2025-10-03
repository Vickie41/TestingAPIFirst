using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models.Entities;

public partial class TbLoanCollateral
{
    public int CollateralId { get; set; }

    public string CollateralType { get; set; } = null!;

    public string CollateralReference { get; set; } = null!;

    public decimal MarketValue { get; set; }

    public decimal ForceSaleValue { get; set; }

    public string Description { get; set; } = null!;

    public string FullAddress { get; set; } = null!;

    public string TownshipCode { get; set; } = null!;

    public int LoanId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TbLoanMaster Loan { get; set; } = null!;
}
