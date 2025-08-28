using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbTownship1
{
    public int TownshipPkid { get; set; }

    public string TownshipCode { get; set; } = null!;

    public string? TownshipName { get; set; }

    public string? DistrictCode { get; set; }

    public string? StateDivisionId { get; set; }
}
