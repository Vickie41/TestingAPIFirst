using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbPayment
{
    public int PaymentPkid { get; set; }

    public string? TransactionId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? OtherCharges { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentHubRefNo { get; set; }

    public string? BankTransactionRefNo { get; set; }

    public string? BankName { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? PaymentStatus { get; set; }

    public bool? IsDeleted { get; set; }

    public int? Foc { get; set; }

    public string? PaymentOption { get; set; }

    public int? CreatedBy { get; set; }
}
