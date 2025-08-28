using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbProduct
{
    public int ProductPkid { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal? ProductPrice { get; set; }

    public decimal? OtherCharges { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
