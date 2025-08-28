using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbRequestFormDetail
{
    public int RequestDetailPkid { get; set; }

    public string? TransactionId { get; set; }

    public string? AttachFileName { get; set; }

    public string? AttachFilePath { get; set; }

    public string? AttachFileType { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }
}
