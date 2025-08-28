using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbUser
{
    public int UserPkid { get; set; }

    public string? UserId { get; set; }

    public string? UserDisplayName { get; set; }

    public string? Password { get; set; }

    public int? OfficePkid { get; set; }

    public int? DepartmentPkid { get; set; }

    public string? UserRole { get; set; }

    public int? StateDivisionPkid { get; set; }

    public int? TownshipPkid { get; set; }

    public string? DepartmentType { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }
}
