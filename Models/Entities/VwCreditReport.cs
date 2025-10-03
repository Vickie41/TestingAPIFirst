using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models.Entities;

public partial class VwCreditReport
{
    public int PersonPkid { get; set; }

    public string NameEnglish { get; set; } = null!;

    public string FullNrc { get; set; } = null!;

    public int? TotalLoans { get; set; }

    public decimal? TotalOutstanding { get; set; }

    public DateTime? LastLoanDate { get; set; }
}
