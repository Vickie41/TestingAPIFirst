using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbRequestForm
{
    public int RequestPkid { get; set; }

    public string? TransactionId { get; set; }

    public string? SerialNo { get; set; }

    public string? RequestType { get; set; }

    public int? ProductPkid { get; set; }

    public string? MinistryName { get; set; }

    public string? OfficeName { get; set; }

    public string? Description { get; set; }

    public DateTime? RequestDate { get; set; }

    public int? CompanyPkid { get; set; }

    public string? Status { get; set; }

    public string? ReceiptStatus { get; set; }

    public DateTime? ReceiptDate { get; set; }

    public DateTime? ExportDate { get; set; }

    public string? Qrcode { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public string? TaxOffice { get; set; }

    public int? TaxOfficePkid { get; set; }

    public string? Township { get; set; }

    public string? RejectStatus { get; set; }
}
