using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbOfficer
{
    public int TaxOfficePkid { get; set; }

    public string? TaxOffice { get; set; }

    public string? LetterNo { get; set; }

    public string? TaxOfficeEnglish { get; set; }

    public string? ShortEngName { get; set; }
}
