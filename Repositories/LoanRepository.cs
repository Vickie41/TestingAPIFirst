using FirstTestingAPI.Data;
using FirstTestingAPI.Models.Entities;
using FirstTestingAPI.Models.Requests;
using FirstTestingAPI.Models.Responses;
using FirstTestingAPI.Helpers;
using FirstTestingAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTestingAPI.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanDbContext _context;
        private readonly ILogger<LoanRepository> _logger;

        private readonly string[] _validGenders = { "M", "F", "O" };
        private readonly string[] _validMaritals = { "S", "M", "D", "W", "U", "P", "F" };
        private readonly string[] _validIdTypes = { "N", "P" };
        private readonly string[] _validNidTypes = { "N", "P", "M", "C", "AC", "NC", "V" };

        public LoanRepository(LoanDbContext context, ILogger<LoanRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Create Loan
        public async Task<LoanCreateResponse> CreateLoanAsync(CreateLoanRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // --- Step 1: Validate Customer Fields ---
                string gender = ValidateGender(request.CustomerInformation.Gender);
                string marital = ValidateMarital(request.CustomerInformation.Marital);
                string idType = ValidateIDType(request.CustomerInformation.IDType);
                string nidType = ValidateNIDType(request.CustomerInformation.NIDType);

                // --- Step 2: Check existing customer ---
                var existingCustomer = await _context.TbPersonalInformations
                    .FirstOrDefaultAsync(p => p.Nrcregion == request.CustomerInformation.NRCRegion &&
                                              p.Nidtype == nidType &&
                                              p.Nrcnumber == request.CustomerInformation.NRCNumber);

                TbPersonalInformation customer;

                if (existingCustomer != null)
                {
                    existingCustomer.UpdateFromCustomerInformation(request.CustomerInformation);
                    existingCustomer.Gender = gender;
                    existingCustomer.Marital = marital;
                    existingCustomer.Idtype = idType;
                    existingCustomer.Nidtype = nidType;
                    existingCustomer.Nationality = "Myanmar";
                    existingCustomer.Occupation = request.CustomerInformation.Occupation ?? "Unknown";
                    existingCustomer.Race = "Bamar";
                    existingCustomer.Phone = existingCustomer.Phone ?? "0000000000";
                    existingCustomer.FullAddress = existingCustomer.FullAddress ?? "N/A";
                    _context.TbPersonalInformations.Update(existingCustomer);
                    customer = existingCustomer;
                }
                else
                {
                    customer = new TbPersonalInformation
                    {
                        NameEnglish = string.IsNullOrWhiteSpace(request.CustomerInformation.NameEnglish)
                                      ? "Unknown" : request.CustomerInformation.NameEnglish,
                        NameMm = string.IsNullOrWhiteSpace(request.CustomerInformation.NameMM)
                                      ? "Unknown" : request.CustomerInformation.NameMM,
                        Nrcregion = request.CustomerInformation.NRCRegion ?? "00",
                        Nrcnumber = request.CustomerInformation.NRCNumber ?? "000000",
                        Nidtype = nidType,
                        FatherNameEnglish = request.CustomerInformation.FatherNameEnglish ?? "Unknown",
                        FatherNameMm = request.CustomerInformation.FatherNameMM ?? "Unknown",
                        Dob = DateOnly.FromDateTime(request.CustomerInformation.DOB),
                        Gender = gender,
                        Marital = marital,
                        Idtype = idType,
                        Nationality = "Myanmar",
                        Occupation = request.CustomerInformation.Occupation ?? "Unknown",
                        Race = "Bamar",
                        Phone = request.CustomerInformation.Phone ?? "0000000000",
                        FullAddress = request.CustomerInformation.Address?.FullAddress ?? "N/A"
                    };
                    await _context.TbPersonalInformations.AddAsync(customer);
                }

                await _context.SaveChangesAsync();

                // --- Step 3: Generate unique SeparateAccountNo ---
                string baseAccountNo = "LN-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                int accSuffix = 0;
                string separateAccountNo;
                do
                {
                    accSuffix++;
                    separateAccountNo = accSuffix == 1 ? baseAccountNo : $"{baseAccountNo}-{accSuffix:D3}";
                } while (await _context.TbLoanMasters.AnyAsync(l => l.SeparateAccountNo == separateAccountNo));

                // --- Step 4: Generate unique OrganizationLoanID ---
                string baseOrgLoanID = $"ORG-{customer.PersonPkid}";
                int orgSuffix = 0;
                string organizationLoanID;
                do
                {
                    orgSuffix++;
                    organizationLoanID = orgSuffix == 1 ? baseOrgLoanID : $"{baseOrgLoanID}-{orgSuffix:D3}";
                } while (await _context.TbLoanMasters.AnyAsync(l => l.OrganizationLoanId == organizationLoanID));

                // --- Step 5: Map Loan safely ---
                var loan = new TbLoanMaster
                {
                    PersonPkid = customer.PersonPkid,
                    OrganizationLoanId = organizationLoanID,
                    BranchName = request.LoanAccount.BranchName ?? "DefaultBranch",
                    SeparateAccountNo = separateAccountNo,
                    ApplicantTypeCode = new[] { "B", "C", "P" }.Contains(request.LoanAccount.ApplicantTypeCode)
                                        ? request.LoanAccount.ApplicantTypeCode
                                        : "P",
                    ProductTypeCode = request.LoanAccount.ProductTypeCode ?? "STL",
                    ProductStatusCode = new[] { "D", "C", "A", "S" }.Contains(request.LoanAccount.ProductStatusCode)
                                        ? request.LoanAccount.ProductStatusCode
                                        : "S",
                    PrincipalAmount = request.LoanAccount.PrincipalAmount > 0
                                        ? request.LoanAccount.PrincipalAmount
                                        : 1000000,
                    DisbursedAmount = request.LoanAccount.DisbursedAmount > 0
                                        ? request.LoanAccount.DisbursedAmount
                                        : 500000,
                    DisbursedDate = DateOnly.FromDateTime(request.LoanAccount.DisbursedDate),
                    ExpiredDate = DateOnly.FromDateTime(request.LoanAccount.ExpiredDate),
                    InterestRate = request.LoanAccount.InterestRate > 0
                                     ? Convert.ToDecimal(request.LoanAccount.InterestRate)
                                     : 2.5m,
                    PrincipalInstalmentAmount = request.LoanAccount.PrincipalInstalmentAmount > 0
                                                ? request.LoanAccount.PrincipalInstalmentAmount
                                                : 100000,
                    PrincipalPaymentFrequency = new[] { "Y", "Q", "M" }.Contains(request.LoanAccount.PrincipalPaymentFrequency)
                                                ? request.LoanAccount.PrincipalPaymentFrequency
                                                : "M",
                    InterestInstalmentAmount = request.LoanAccount.InterestInstalmentAmount > 0
                                                ? request.LoanAccount.InterestInstalmentAmount
                                                : 5000,
                    InterestPaymentFrequency = new[] { "Y", "Q", "M" }.Contains(request.LoanAccount.InterestPaymentFrequency)
                                                ? request.LoanAccount.InterestPaymentFrequency
                                                : "M",
                    PrincipalOverdueAmount = request.LoanAccount.PrincipalOverdueAmount,
                    InterestOverdueAmount = request.LoanAccount.InterestOverdueAmount,
                    PrincipalOutstandingAmount = request.LoanAccount.PrincipalOutstandingAmount,
                    InterestOutstandingAmount = request.LoanAccount.InterestOutstandingAmount,
                    Tenure = request.LoanAccount.Tenure > 0 ? request.LoanAccount.Tenure : 12,
                    AccountTypeCode = new[] { "J", "S" }.Contains(request.LoanAccount.AccountTypeCode)
                                        ? request.LoanAccount.AccountTypeCode
                                        : "S",
                    SmeFlag = request.LoanAccount.SME_Flag,
                    IndustrialSectorCode = request.LoanAccount.IndustrialSectorCode ?? "00",
                    LastPaymentAmount = request.LoanAccount.LastPaymentAmount ?? 0,
                    LastPaymentDate = request.LoanAccount.LastPaymentDate ?? DateTime.Now,
                    LastPaymentFor = new[] { "A", "I", "P" }.Contains(request.LoanAccount.LastPaymentFor)
                                        ? request.LoanAccount.LastPaymentFor
                                        : "A"
                };

                await _context.TbLoanMasters.AddAsync(loan);
                await _context.SaveChangesAsync();

                // --- Step 6: Generate unique CollateralReference ---
                string baseCollateralRef = $"COL-{customer.PersonPkid}";
                int collSuffix = 0;
                string collateralReference;
                do
                {
                    collSuffix++;
                    collateralReference = collSuffix == 1 ? baseCollateralRef : $"{baseCollateralRef}-{collSuffix:D3}";
                } while (await _context.TbLoanCollaterals.AnyAsync(c => c.CollateralReference == collateralReference));

                var collateral = new TbLoanCollateral
                {
                    LoanId = loan.LoanId,
                    CollateralType = new[] { "OT", "EQ", "VD", "LT" }.Contains(request.LoanCollateral.CollateralType)
                                        ? request.LoanCollateral.CollateralType
                                        : "LT",
                    CollateralReference = collateralReference,
                    MarketValue = request.LoanCollateral.MarketValue > 0
                                        ? request.LoanCollateral.MarketValue
                                        : 1000000,
                    ForceSaleValue = request.LoanCollateral.ForceSaleValue > 0
                                        ? request.LoanCollateral.ForceSaleValue
                                        : 800000,
                    Description = request.LoanCollateral.Description ?? "N/A",
                    FullAddress = request.LoanCollateral.FullAddress ?? "N/A",
                    TownshipCode = request.LoanCollateral.TownshipCode ?? "00"
                };

                await _context.TbLoanCollaterals.AddAsync(collateral);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                string fullNrc = $"{customer.Nrcregion}({customer.Nidtype}){customer.Nrcnumber}";

                return new LoanCreateResponse
                {
                    Message = $"{fullNrc} loan account is successfully added.",
                    CustomerNameEnglish = customer.NameEnglish,
                    CustomerNameMyanmar = customer.NameMm,
                    NRCNumber = fullNrc,
                    DOB = customer.Dob.ToDateTime(TimeOnly.MinValue).ToString("M/d/yyyy"),
                    FatherNameEnglish = customer.FatherNameEnglish,
                    FatherNameMyanmar = customer.FatherNameMm,
                    MCISAccountNumber = $"MCIS-{loan.LoanId:D8}",
                    OrganizationLoanID = loan.OrganizationLoanId,
                    BranchName = loan.BranchName
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating loan");
                throw;
            }
        }







        #endregion

        #region Interface placeholders
        public Task<List<LoanDetailsResponse>> SearchLoansAsync(SearchLoansRequest request) =>
            throw new NotImplementedException();

        public Task<RepaymentResponse> ProcessRepaymentAsync(RepaymentRequest request) =>
            throw new NotImplementedException();

        public Task<LoanDetailsResponse> GetCreditHistoryByNRCAsync(string nrc) =>
            throw new NotImplementedException();
        #endregion

        #region Validation Helpers
        private string ValidateGender(string gender) =>
            _validGenders.Contains(gender?.ToUpper()) ? gender.ToUpper() : "M";

        private string ValidateMarital(string marital) =>
            _validMaritals.Contains(marital?.ToUpper()) ? marital.ToUpper() : "S";

        private string ValidateIDType(string idType) =>
            _validIdTypes.Contains(idType?.ToUpper()) ? idType.ToUpper() : "N";

        private string ValidateNIDType(string nidType) =>
            _validNidTypes.Contains(nidType?.ToUpper()) ? nidType.ToUpper() : "N";
        #endregion
    }
}
