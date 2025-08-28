using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbOffice
{
    public int OfficePkid { get; set; }

    public string? OfficeName { get; set; }

    public string? OfficeAddress { get; set; }

    public string? TownshipCode { get; set; }

    public string? StateDivisionCode { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsDeleted { get; set; }
}
