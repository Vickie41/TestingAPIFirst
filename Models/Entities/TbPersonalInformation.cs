using System;
using System.Collections.Generic;

namespace FirstTestingAPI.Models.Entities;

public partial class TbPersonalInformation
{
    internal string StateDivisionID;
    internal string TownshipID;

    public int PersonPkid { get; set; }

    public string NameEnglish { get; set; } = null!;

    public string? NameMm { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string FatherNameEnglish { get; set; } = null!;

    public string? FatherNameMm { get; set; }

    public string Marital { get; set; } = null!;

    public string Idtype { get; set; } = null!;

    public string Nidtype { get; set; } = null!;

    public string Nrcregion { get; set; } = null!;

    public string Nrcnumber { get; set; } = null!;

    public string Race { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string Occupation { get; set; } = null!;

    public string? Idcnumber { get; set; }

    public DateTime? IdcexpireAt { get; set; }

    public string Phone { get; set; } = null!;

    public string? AddressTypeCode { get; set; }

    public string? StateOrRegionCode { get; set; }

    public string? TownshipCode { get; set; }

    public string? FullAddress { get; set; }

    public string? PostalCode { get; set; }

    public string? AddressRemark { get; set; }

    public string? SpouseNameEng { get; set; }

    public string? SpouseNameMm { get; set; }

    public string? SpouseIdtype { get; set; }

    public string? SpouseNidtype { get; set; }

    public string? SpouseNrcRegion { get; set; }

    public string? SpouseNrcNo { get; set; }

    public string? SpouseIdcardNo { get; set; }

    public DateTime? SpouseIdcardExpireAt { get; set; }

    public string? TransactionId { get; set; }

    public string? AccountNumber { get; set; }

    public string? Jicanumber { get; set; }

    public string? AccountType { get; set; }

    public string? RegionId { get; set; }

    public string? TownshipId { get; set; }

    public string? StateDivisionId { get; set; }

    public string? RegistrationDate { get; set; }

    public bool? IsMainPerson { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsRecordEdited { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<TbLoanMaster> TbLoanMasters { get; set; } = new List<TbLoanMaster>();
}
