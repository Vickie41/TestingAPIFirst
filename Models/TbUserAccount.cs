using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbUserAccount
{
    public int UserPkid { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Otpcode { get; set; }

    public DateTime? Otpexpired { get; set; }

    public int? Township { get; set; }

    public int? StateDivision { get; set; }

    public string? UserType { get; set; }

    public string? UserRole { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }
}
