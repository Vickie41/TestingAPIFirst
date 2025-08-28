using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbFinancialYear
{
    public int FinancialYearPkid { get; set; }

    public DateTime? FinancialStartDate { get; set; }

    public DateTime? FinancialMiddleDate { get; set; }

    public DateTime? FinancialEndDate { get; set; }

    public string? FinancialYear { get; set; }

    public bool? IsActiveFinancialYear { get; set; }

    public bool? IsAccountClosed { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsUploaded { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }
}
