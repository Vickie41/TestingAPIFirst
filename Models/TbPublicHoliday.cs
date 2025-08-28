using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbPublicHoliday
{
    public int PublicHolidayPkid { get; set; }

    public DateTime? Holiday { get; set; }

    public string? Description { get; set; }

    public string? DayName { get; set; }
}
