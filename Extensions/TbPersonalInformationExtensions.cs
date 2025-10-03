using FirstTestingAPI.Models.Entities;
using FirstTestingAPI.Models.Requests;
using System;

namespace FirstTestingAPI.Extensions
{
    public static class TbPersonalInformationExtensions
    {
        public static void UpdateFromCustomerInformation(this TbPersonalInformation customer, CustomerInformation info)
        {
            customer.NameEnglish = info.NameEnglish;
            customer.NameMm = info.NameMM;
            customer.Nrcregion = info.NRCRegion;
            customer.Nrcnumber = info.NRCNumber;
            customer.Nidtype = info.NIDType;
            customer.FatherNameEnglish = info.FatherNameEnglish;
            customer.FatherNameMm = info.FatherNameMM;
            customer.Dob = DateOnly.FromDateTime(info.DOB);
            customer.Gender = info.Gender;
            customer.Marital = info.Marital;
            customer.Idtype = info.IDType;
            customer.Phone = info.Phone;
            customer.FullAddress = info.Address?.FullAddress;
        }
    }
}
