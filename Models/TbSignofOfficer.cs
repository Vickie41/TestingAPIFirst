using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbSignofOfficer
{
    public int OfficerPkid { get; set; }

    public string? TaxOffice { get; set; }

    public string? LetterNo { get; set; }

    public string? Sign { get; set; }

    public string? InitialSign { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? TaxOfficePkid { get; set; }

    public bool? IsActive { get; set; }
}
