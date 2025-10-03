USE CreditBureauTesting;
GO

-- Create TbPersonalInformation table
CREATE TABLE TbPersonalInformation (
    PersonPkid INT IDENTITY(1,1) PRIMARY KEY,
    NameEnglish NVARCHAR(100) NOT NULL,
    NameMM NVARCHAR(100) NULL,
    Gender NCHAR(1) NOT NULL CHECK (Gender IN ('M', 'F')),
    DOB DATE NOT NULL,
    FatherNameEnglish NVARCHAR(100) NOT NULL,
    FatherNameMM NVARCHAR(100) NULL,
    Marital NCHAR(1) NOT NULL CHECK (Marital IN ('M', 'S')),
    IDType NCHAR(1) NOT NULL CHECK (IDType IN ('N', 'O')), -- N=NRC, O=Other
    NIDType NCHAR(1) NOT NULL CHECK (NIDType IN ('C', 'F', 'A')), -- C=Citizen, F=Foreigner, A=Associate
    NRCRegion NVARCHAR(50) NOT NULL,
    NRCNumber NVARCHAR(20) NOT NULL,
    Race NVARCHAR(50) NOT NULL,
    Nationality NVARCHAR(50) NOT NULL,
    Occupation NVARCHAR(100) NOT NULL,
    IDCNumber NVARCHAR(50) NULL,
    IDCExpireAt DATETIME2 NULL,
    Phone NVARCHAR(20) NOT NULL,
    
    -- Address fields
    AddressTypeCode NVARCHAR(10) NULL,
    StateOrRegionCode NVARCHAR(20) NULL,
    TownshipCode NVARCHAR(20) NULL,
    FullAddress NVARCHAR(500) NULL,
    PostalCode NVARCHAR(20) NULL,
    AddressRemark NVARCHAR(200) NULL,
    
    -- Spouse information
    SpouseNameEng NVARCHAR(100) NULL,
    SpouseNameMM NVARCHAR(100) NULL,
    SpouseIDType NVARCHAR(10) NULL,
    SpouseNIDType NVARCHAR(20) NULL,
    SpouseNRC_Region NVARCHAR(10) NULL,
    SpouseNRC_No NVARCHAR(50) NULL,
    SpouseIDCard_No NVARCHAR(50) NULL,
    SpouseIDCard_ExpireAt DATETIME2 NULL,
    
    -- Legacy fields
    TransactionId NVARCHAR(100) NULL,
    AccountNumber NVARCHAR(100) NULL,
    Jicanumber NVARCHAR(100) NULL,
    AccountType NVARCHAR(50) NULL,
    RegionId NVARCHAR(50) NULL,
    TownshipId NVARCHAR(50) NULL,
    StateDivisionId NVARCHAR(50) NULL,
    RegistrationDate NVARCHAR(50) NULL,
    IsMainPerson BIT NULL DEFAULT 0,
    IsActive BIT NULL DEFAULT 1,
    IsDeleted BIT NULL DEFAULT 0,
    IsRecordEdited BIT NULL DEFAULT 0,
    CreatedBy NVARCHAR(100) NULL,
    CreatedDate DATETIME2 NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL,
    
    -- Indexes for performance
    CONSTRAINT IX_TbPersonalInformation_NRC UNIQUE (NRCRegion, NIDType, NRCNumber),
    CONSTRAINT IX_TbPersonalInformation_Phone UNIQUE (Phone)
);
GO

-- Create TbLoanMaster table
CREATE TABLE TbLoanMaster (
    LoanId INT IDENTITY(1,1) PRIMARY KEY,
    OrganizationLoanID NVARCHAR(50) NOT NULL,
    BranchName NVARCHAR(100) NOT NULL,
    SeparateAccountNo NVARCHAR(50) NOT NULL,
    ApplicantTypeCode NCHAR(1) NOT NULL CHECK (ApplicantTypeCode IN ('P', 'C', 'B')), -- P=Personal, C=Corporate, B=Business
    ProductTypeCode NVARCHAR(10) NOT NULL, -- STL, HLS, etc.
    ProductStatusCode NCHAR(1) NOT NULL CHECK (ProductStatusCode IN ('S', 'A', 'C', 'D')), -- S=Standard, A=Active, C=Closed, D=Default
    PrincipalAmount DECIMAL(18,2) NOT NULL,
    DisbursedAmount DECIMAL(18,2) NOT NULL,
    DisbursedDate DATE NOT NULL,
    ExpiredDate DATE NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL,
    PrincipalInstalmentAmount DECIMAL(18,2) NOT NULL,
    PrincipalPaymentFrequency NCHAR(1) NOT NULL CHECK (PrincipalPaymentFrequency IN ('M', 'Q', 'Y')), -- M=Monthly, Q=Quarterly, Y=Yearly
    InterestInstalmentAmount DECIMAL(18,2) NOT NULL,
    InterestPaymentFrequency NCHAR(1) NOT NULL CHECK (InterestPaymentFrequency IN ('M', 'Q', 'Y')),
    PrincipalOverdueAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    InterestOverdueAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    PrincipalOutstandingAmount DECIMAL(18,2) NOT NULL,
    InterestOutstandingAmount DECIMAL(18,2) NOT NULL,
    Tenure INT NOT NULL,
    AccountTypeCode NCHAR(1) NOT NULL CHECK (AccountTypeCode IN ('S', 'J')), -- S=Single, J=Joint
    SME_Flag BIT NOT NULL DEFAULT 0,
    IndustrialSectorCode NVARCHAR(10) NULL,
    LastPaymentAmount DECIMAL(18,2) NULL,
    LastPaymentDate DATETIME2 NULL,
    LastPaymentFor NCHAR(1) NULL CHECK (LastPaymentFor IN ('P', 'I', 'A')), -- P=Principal, I=Interest, A=All
    
    -- Foreign key to personal information
    PersonPkid INT NOT NULL,
    
    -- System fields
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL,
    
    -- Constraints
    CONSTRAINT FK_TbLoanMaster_TbPersonalInformation FOREIGN KEY (PersonPkid) REFERENCES TbPersonalInformation(PersonPkid),
    CONSTRAINT IX_TbLoanMaster_OrganizationLoanID UNIQUE (OrganizationLoanID),
    CONSTRAINT IX_TbLoanMaster_SeparateAccountNo UNIQUE (SeparateAccountNo)
);
GO

-- Create TbLoanCollateral table
CREATE TABLE TbLoanCollateral (
    CollateralId INT IDENTITY(1,1) PRIMARY KEY,
    CollateralType NVARCHAR(10) NOT NULL CHECK (CollateralType IN ('LT', 'VD', 'EQ', 'OT')), -- LT=Land Title, VD=Vehicle, EQ=Equipment, OT=Other
    CollateralReference NVARCHAR(50) NOT NULL,
    MarketValue DECIMAL(18,2) NOT NULL,
    ForceSaleValue DECIMAL(18,2) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    FullAddress NVARCHAR(500) NOT NULL,
    TownshipCode NVARCHAR(20) NOT NULL,
    
    -- Foreign key to loan
    LoanId INT NOT NULL,
    
    -- System fields
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL,
    
    -- Constraints
    CONSTRAINT FK_TbLoanCollateral_TbLoanMaster FOREIGN KEY (LoanId) REFERENCES TbLoanMaster(LoanId),
    CONSTRAINT IX_TbLoanCollateral_Reference UNIQUE (CollateralReference)
);
GO

-- Create TbLoanReturnTransactionDetail table
CREATE TABLE TbLoanReturnTransactionDetail (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    MCISAccountNumber NVARCHAR(100) NOT NULL,
    RepaymentAmount DECIMAL(18,2) NOT NULL,
    RepaymentFor NCHAR(1) NOT NULL CHECK (RepaymentFor IN ('P', 'I', 'A')), -- P=Principal, I=Interest, A=All
    RepaymentDate DATE NOT NULL,
    OrganizationRepaymentId NVARCHAR(50) NOT NULL,
    MCISRepaymentNumber NVARCHAR(100) NOT NULL,
    
    -- Foreign key to loan
    LoanId INT NOT NULL,
    
    -- System fields
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    -- Constraints
    CONSTRAINT FK_TbLoanReturnTransactionDetail_TbLoanMaster FOREIGN KEY (LoanId) REFERENCES TbLoanMaster(LoanId),
    CONSTRAINT IX_TbLoanReturnTransactionDetail_OrgRepaymentId UNIQUE (OrganizationRepaymentId),
    CONSTRAINT IX_TbLoanReturnTransactionDetail_MCISRepayment UNIQUE (MCISRepaymentNumber)
);
GO

-- Create indexes for better performance
CREATE INDEX IX_TbPersonalInformation_CreatedDate ON TbPersonalInformation(CreatedDate);
CREATE INDEX IX_TbPersonalInformation_IsActive ON TbPersonalInformation(IsActive);
CREATE INDEX IX_TbLoanMaster_PersonPkid ON TbLoanMaster(PersonPkid);
CREATE INDEX IX_TbLoanMaster_DisbursedDate ON TbLoanMaster(DisbursedDate);
CREATE INDEX IX_TbLoanMaster_ExpiredDate ON TbLoanMaster(ExpiredDate);
CREATE INDEX IX_TbLoanCollateral_LoanId ON TbLoanCollateral(LoanId);
CREATE INDEX IX_TbLoanReturnTransactionDetail_LoanId ON TbLoanReturnTransactionDetail(LoanId);
CREATE INDEX IX_TbLoanReturnTransactionDetail_RepaymentDate ON TbLoanReturnTransactionDetail(RepaymentDate);
GO

-- Insert sample data for testing
INSERT INTO TbPersonalInformation (
    NameEnglish, NameMM, Gender, DOB, FatherNameEnglish, FatherNameMM, 
    Marital, IDType, NIDType, NRCRegion, NRCNumber, Race, Nationality, 
    Occupation, IDCNumber, IDCExpireAt, Phone, AddressTypeCode, 
    StateOrRegionCode, TownshipCode, FullAddress, PostalCode
) VALUES (
    'Aung Soe', N'အောင်စိုး', 'M', '1990-05-12', 'Tun Aung', N'တွန်းအောင်',
    'M', 'N', 'C', '9/PaKaKha', '123458', 'Bamar', 'Myanmar',
    'Software Engineer', '123456', '2030-12-31', '+959761234567', 'RESID',
    'MMR001', 'MMR001013', 'No. 123, Thamine Street, Kamayut Township, Yangon', '11041'
);
GO

-- Create a stored procedure for loan creation
CREATE PROCEDURE sp_CreateLoan
    @CustomerInformation NVARCHAR(MAX),
    @LoanAccount NVARCHAR(MAX),
    @LoanCollateral NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Parse JSON and insert data (implementation would use JSON functions)
        -- This is a simplified version - actual implementation would parse JSON
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- Create a view for loan details (FIXED - removed FullNRC reference)
CREATE VIEW vw_LoanDetails AS
SELECT 
    p.NameEnglish,
    p.NameMM,
    p.Gender,
    p.DOB,
    p.NRCRegion + '(' + p.NIDType + ')' + p.NRCNumber AS FullNRC,
    p.FatherNameEnglish,
    p.FatherNameMM,
    p.Marital,
    p.Race,
    p.Nationality,
    p.Phone,
    l.OrganizationLoanID,
    l.BranchName,
    l.SeparateAccountNo,
    l.ProductTypeCode,
    l.ProductStatusCode,
    l.DisbursedAmount,
    l.DisbursedDate,
    l.PrincipalOutstandingAmount,
    l.PrincipalOverdueAmount,
    l.InterestOutstandingAmount,
    l.InterestOverdueAmount,
    l.InterestRate,
    l.Tenure,
    l.ExpiredDate,
    l.LastPaymentDate,
    l.CreatedDate AS LoanCreatedDate
FROM TbPersonalInformation p
INNER JOIN TbLoanMaster l ON p.PersonPkid = l.PersonPkid
WHERE p.IsActive = 1 AND p.IsDeleted = 0;
GO

-- Create a view for credit reports (FIXED - removed FullNRC reference)
CREATE VIEW vw_CreditReports AS
SELECT 
    p.PersonPkid,
    p.NameEnglish,
    p.NRCRegion + '(' + p.NIDType + ')' + p.NRCNumber AS FullNRC,
    COUNT(l.LoanId) AS TotalLoans,
    SUM(l.PrincipalOutstandingAmount) AS TotalOutstanding,
    MAX(l.CreatedDate) AS LastLoanDate
FROM TbPersonalInformation p
LEFT JOIN TbLoanMaster l ON p.PersonPkid = l.PersonPkid
WHERE p.IsActive = 1 AND p.IsDeleted = 0
GROUP BY p.PersonPkid, p.NameEnglish, p.NRCRegion, p.NIDType, p.NRCNumber;
GO

PRINT 'Database CreditBureauTesting created successfully with all required tables!';