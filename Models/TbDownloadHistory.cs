using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbDownloadHistory
{
    public int DownloadPkid { get; set; }

    public int? RequestPkid { get; set; }

    public DateTime? DownloadTime { get; set; }

    public int? DownloadCount { get; set; }
}
