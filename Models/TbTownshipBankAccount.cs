using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbTownshipBankAccount
{
    public int BankAccountPkid { get; set; }

    public string? BankAccountId { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? BankName { get; set; }

    public string? BankTownshipId { get; set; }

    public string? TownshipId { get; set; }

    public bool? IsDeleted { get; set; }
}
