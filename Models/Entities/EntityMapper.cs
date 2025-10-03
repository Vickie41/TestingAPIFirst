using FirstTestingAPI.Models.Requests;
using FirstTestingAPI.Models.Entities;
using System;

namespace FirstTestingAPI.Helpers
{
    public static class EntityMapper
    {
        public static TbPersonalInformation MapFromCustomerInformation(CustomerInformation info)
        {
            return new TbPersonalInformation
            {
                NameEnglish = info.NameEnglish,
                NameMm = info.NameMM,
                Nrcregion = info.NRCRegion,
                Nrcnumber = info.NRCNumber,
                Nidtype = info.NIDType,
                FatherNameEnglish = info.FatherNameEnglish,
                FatherNameMm = info.FatherNameMM,
                Dob = DateOnly.FromDateTime(info.DOB),
                Gender = info.Gender,
                Marital = info.Marital,
                Idtype = info.IDType,
                Phone = info.Phone,
                FullAddress = info.Address?.FullAddress
            };
        }

        public static TbLoanMaster MapLoanMaster(LoanAccount account, TbPersonalInformation customer)
        {
            return new TbLoanMaster
            {
                PersonPkid = customer.PersonPkid,
                OrganizationLoanId = account.OrganizationLoanID,
                BranchName = account.BranchName,
                SeparateAccountNo = account.SeparateAccountNo,
                ApplicantTypeCode = account.ApplicantTypeCode,       // Must be B/C/P
                ProductTypeCode = account.ProductTypeCode,
                ProductStatusCode = account.ProductStatusCode,       // D/C/A/S
                PrincipalAmount = account.PrincipalAmount,
                DisbursedAmount = account.DisbursedAmount,
                DisbursedDate = DateOnly.FromDateTime(account.DisbursedDate),
                ExpiredDate = DateOnly.FromDateTime(account.ExpiredDate),
                InterestRate = account.InterestRate,
                PrincipalInstalmentAmount = account.PrincipalInstalmentAmount,
                PrincipalPaymentFrequency = account.PrincipalPaymentFrequency,   // Y/Q/M
                InterestInstalmentAmount = account.InterestInstalmentAmount,
                InterestPaymentFrequency = account.InterestPaymentFrequency,     // Y/Q/M
                PrincipalOverdueAmount = account.PrincipalOverdueAmount,
                InterestOverdueAmount = account.InterestOverdueAmount,
                PrincipalOutstandingAmount = account.PrincipalOutstandingAmount,
                InterestOutstandingAmount = account.InterestOutstandingAmount,
                Tenure = account.Tenure,
                AccountTypeCode = account.AccountTypeCode,                       // J/S
                SmeFlag = account.SME_Flag,
                IndustrialSectorCode = account.IndustrialSectorCode,
                LastPaymentAmount = account.LastPaymentAmount,
                LastPaymentDate = account.LastPaymentDate,
                LastPaymentFor = account.LastPaymentFor                             // A/I/P
            };
        }

        public static TbLoanCollateral MapLoanCollateral(LoanCollateral collateral, int loanId)
        {
            return new TbLoanCollateral
            {
                LoanId = loanId,
                CollateralType = collateral.CollateralType,
                CollateralReference = collateral.CollateralReference,
                MarketValue = collateral.MarketValue,
                ForceSaleValue = collateral.ForceSaleValue,
                Description = collateral.Description,
                FullAddress = collateral.FullAddress,
                TownshipCode = collateral.TownshipCode
            };
        }

        public static DateTime? ConvertToDateTime(DateOnly? date)
        {
            if (date == null) return null;
            return date.Value.ToDateTime(TimeOnly.MinValue);
        }

        public static string GenerateMCISAccountNumber(TbPersonalInformation customer, TbLoanMaster loan)
        {
            return $"MCIS-{loan.LoanId:D8}";
        }
    }
}
