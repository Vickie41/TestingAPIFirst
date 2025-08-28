using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbCompanyInfo
{
    public int CompanyPkid { get; set; }

    public string CompanyName { get; set; } = null!;

    public string RegistrationNo { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public string? CompanyType { get; set; }

    public string? Address { get; set; }

    public string? State { get; set; }

    public string? Town { get; set; }

    public string? Street { get; set; }

    public string? Quarter { get; set; }

    public string? HousingNo { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? PhoneNo { get; set; }

    public int? Township { get; set; }

    public int? StateDivision { get; set; }

    public bool? Dicaverified { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? BusinessStartDate { get; set; }

    public string? TaxOffice { get; set; }
}
