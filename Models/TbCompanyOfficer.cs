using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbCompanyOfficer
{
    public int OfficerPkid { get; set; }

    public string OfficerName { get; set; } = null!;

    public string? PositionType { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Nrcno { get; set; }

    public string? PassportNo { get; set; }

    public string? Gender { get; set; }

    public string? Nationality { get; set; }

    public string? PhoneNo { get; set; }

    public string? Email { get; set; }

    public int? CompanyPkid { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }
}
