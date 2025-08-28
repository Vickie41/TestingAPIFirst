using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbStateDivision
{
    public int StateDivisionPkid { get; set; }

    public string StateDivisionCode { get; set; } = null!;

    public string? StateDivisionName { get; set; }

    public string? CityOfRegion { get; set; }

    public string? EngShortCode { get; set; }

    public string? MynShortCode { get; set; }
}
