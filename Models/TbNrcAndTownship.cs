using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbNrcAndTownship
{
    public int NrcAndTownshipPkid { get; set; }

    public string? NrcinitialCodeEnglish { get; set; }

    public string? NrcinitialCodeMyanmar { get; set; }

    public string? NrctownshipCodeEng { get; set; }

    public string? NrctownshipCodeMyn { get; set; }

    public string? PresentTownship { get; set; }

    public string? TownshipDigitCode { get; set; }

    public bool? IsDeleted { get; set; }
}
