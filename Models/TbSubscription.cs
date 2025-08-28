using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbSubscription
{
    public int SubPkid { get; set; }

    public int UserPkid { get; set; }

    public string? SubType { get; set; }

    public int? FreeLimit { get; set; }

    public DateTime? PremiumDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? CreatedDate { get; set; }
}
