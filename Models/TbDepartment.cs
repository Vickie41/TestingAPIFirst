using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models;

public partial class TbDepartment
{
    public int DepartmentPkid { get; set; }

    public string? DepartmentName { get; set; }

    public int? OfficePkid { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public string? StateDivisionCode { get; set; }
}
