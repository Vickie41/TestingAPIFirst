using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FirstTestingAPI.Models;

public partial class TaxClearSystemContext : DbContext
{
    public TaxClearSystemContext()
    {
    }

    public TaxClearSystemContext(DbContextOptions<TaxClearSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCompanyInfo> TbCompanyInfos { get; set; }

    public virtual DbSet<TbCompanyOfficer> TbCompanyOfficers { get; set; }

    public virtual DbSet<TbDepartment> TbDepartments { get; set; }

    public virtual DbSet<TbDownloadHistory> TbDownloadHistories { get; set; }

    public virtual DbSet<TbFinancialYear> TbFinancialYears { get; set; }

    public virtual DbSet<TbNrcAndTownship> TbNrcAndTownships { get; set; }

    public virtual DbSet<TbOffice> TbOffices { get; set; }

    public virtual DbSet<TbOfficer> TbOfficers { get; set; }

    public virtual DbSet<TbPayment> TbPayments { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbPublicHoliday> TbPublicHolidays { get; set; }

    public virtual DbSet<TbRequestForm> TbRequestForms { get; set; }

    public virtual DbSet<TbRequestFormDetail> TbRequestFormDetails { get; set; }

    public virtual DbSet<TbSignofOfficer> TbSignofOfficers { get; set; }

    public virtual DbSet<TbStateDivision> TbStateDivisions { get; set; }

    public virtual DbSet<TbStep> TbSteps { get; set; }

    public virtual DbSet<TbSubscription> TbSubscriptions { get; set; }

    public virtual DbSet<TbTownship> TbTownships { get; set; }

    public virtual DbSet<TbTownship1> TbTownship1s { get; set; }

    public virtual DbSet<TbTownshipBankAccount> TbTownshipBankAccounts { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    public virtual DbSet<TbUserAccount> TbUserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=203.81.89.218; Database=TaxClearSystem; User Id=madbadmin; Password=@dmin123;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCompanyInfo>(entity =>
        {
            entity.HasKey(e => e.CompanyPkid);

            entity.ToTable("TB_CompanyInfo");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BusinessStartDate).HasColumnType("datetime");
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.CompanyType).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dicaverified).HasColumnName("DICAVerified");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.HousingNo).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNo).HasMaxLength(50);
            entity.Property(e => e.Quarter).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.RegistrationNo).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TaxOffice).HasMaxLength(50);
            entity.Property(e => e.Town).HasMaxLength(50);
            entity.Property(e => e.Website).HasMaxLength(50);
        });

        modelBuilder.Entity<TbCompanyOfficer>(entity =>
        {
            entity.HasKey(e => e.OfficerPkid);

            entity.ToTable("TB_CompanyOfficer");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.Nrcno)
                .HasMaxLength(50)
                .HasColumnName("NRCNo");
            entity.Property(e => e.OfficerName).HasMaxLength(50);
            entity.Property(e => e.PassportNo).HasMaxLength(50);
            entity.Property(e => e.PhoneNo).HasMaxLength(50);
            entity.Property(e => e.PositionType).HasMaxLength(50);
        });

        modelBuilder.Entity<TbDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentPkid);

            entity.ToTable("TB_Department");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DepartmentName).HasMaxLength(500);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.StateDivisionCode).HasMaxLength(2);
        });

        modelBuilder.Entity<TbDownloadHistory>(entity =>
        {
            entity.HasKey(e => e.DownloadPkid);

            entity.ToTable("TB_DownloadHistory");

            entity.Property(e => e.DownloadTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbFinancialYear>(entity =>
        {
            entity.HasKey(e => e.FinancialYearPkid);

            entity.ToTable("TB_FinancialYear");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FinancialEndDate).HasColumnType("datetime");
            entity.Property(e => e.FinancialMiddleDate).HasColumnType("datetime");
            entity.Property(e => e.FinancialStartDate).HasColumnType("datetime");
            entity.Property(e => e.FinancialYear).HasMaxLength(50);
        });

        modelBuilder.Entity<TbNrcAndTownship>(entity =>
        {
            entity.HasKey(e => e.NrcAndTownshipPkid);

            entity.ToTable("TB_NRC_And_Township");

            entity.Property(e => e.NrcAndTownshipPkid).HasColumnName("NRC_And_Township_Pkid");
            entity.Property(e => e.NrcinitialCodeEnglish)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NRCInitialCodeEnglish");
            entity.Property(e => e.NrcinitialCodeMyanmar)
                .HasMaxLength(50)
                .HasColumnName("NRCInitialCodeMyanmar");
            entity.Property(e => e.NrctownshipCodeEng)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NRCTownshipCodeEng");
            entity.Property(e => e.NrctownshipCodeMyn)
                .HasMaxLength(50)
                .HasColumnName("NRCTownshipCodeMyn");
            entity.Property(e => e.PresentTownship).HasMaxLength(100);
            entity.Property(e => e.TownshipDigitCode)
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbOffice>(entity =>
        {
            entity.HasKey(e => e.OfficePkid);

            entity.ToTable("TB_Office");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.OfficeAddress).HasMaxLength(250);
            entity.Property(e => e.OfficeName).HasMaxLength(500);
            entity.Property(e => e.StateDivisionCode).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(50);
        });

        modelBuilder.Entity<TbOfficer>(entity =>
        {
            entity.HasKey(e => e.TaxOfficePkid);

            entity.ToTable("TB_Officer");

            entity.Property(e => e.LetterNo).HasMaxLength(50);
            entity.Property(e => e.ShortEngName).HasMaxLength(50);
            entity.Property(e => e.TaxOffice).HasMaxLength(50);
            entity.Property(e => e.TaxOfficeEnglish).HasMaxLength(50);
        });

        modelBuilder.Entity<TbPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentPkid);

            entity.ToTable("TB_Payments");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankName).HasMaxLength(50);
            entity.Property(e => e.BankTransactionRefNo).HasMaxLength(50);
            entity.Property(e => e.Foc).HasColumnName("FOC");
            entity.Property(e => e.OtherCharges).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentHubRefNo).HasMaxLength(50);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentOption).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .HasColumnName("TransactionID");
        });

        modelBuilder.Entity<TbProduct>(entity =>
        {
            entity.HasKey(e => e.ProductPkid);

            entity.ToTable("TB_Product");

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OtherCharges).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TbPublicHoliday>(entity =>
        {
            entity.HasKey(e => e.PublicHolidayPkid);

            entity.ToTable("TB_PublicHoliday");

            entity.Property(e => e.DayName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Holiday).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbRequestForm>(entity =>
        {
            entity.HasKey(e => e.RequestPkid);

            entity.ToTable("TB_RequestForm");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExportDate).HasColumnType("datetime");
            entity.Property(e => e.MinistryName).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OfficeName).HasMaxLength(100);
            entity.Property(e => e.Qrcode).HasColumnName("QRCode");
            entity.Property(e => e.ReceiptDate).HasColumnType("datetime");
            entity.Property(e => e.ReceiptStatus).HasMaxLength(50);
            entity.Property(e => e.RejectStatus).HasMaxLength(50);
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.RequestType).HasMaxLength(50);
            entity.Property(e => e.SerialNo).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TaxOffice).HasMaxLength(100);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .HasColumnName("TransactionID");
        });

        modelBuilder.Entity<TbRequestFormDetail>(entity =>
        {
            entity.HasKey(e => e.RequestDetailPkid);

            entity.ToTable("TB_RequestFormDetail");

            entity.Property(e => e.AttachFileName).HasMaxLength(1000);
            entity.Property(e => e.AttachFilePath).HasMaxLength(1000);
            entity.Property(e => e.AttachFileType).HasMaxLength(1000);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .HasColumnName("TransactionID");
        });

        modelBuilder.Entity<TbSignofOfficer>(entity =>
        {
            entity.HasKey(e => e.OfficerPkid).HasName("PK_TB_TaxOffice");

            entity.ToTable("TB_SignofOfficer");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LetterNo).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TaxOffice).HasMaxLength(50);
        });

        modelBuilder.Entity<TbStateDivision>(entity =>
        {
            entity.HasKey(e => e.StateDivisionPkid);

            entity.ToTable("TB_StateDivision");

            entity.Property(e => e.CityOfRegion).HasMaxLength(100);
            entity.Property(e => e.EngShortCode).HasMaxLength(10);
            entity.Property(e => e.MynShortCode).HasMaxLength(10);
            entity.Property(e => e.StateDivisionCode).HasMaxLength(2);
            entity.Property(e => e.StateDivisionName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbStep>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TB_Step");

            entity.Property(e => e.Step).HasColumnName("step");
        });

        modelBuilder.Entity<TbSubscription>(entity =>
        {
            entity.HasKey(e => e.SubPkid);

            entity.ToTable("TB_Subscriptions");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumDate).HasColumnType("datetime");
            entity.Property(e => e.SubType).HasMaxLength(50);
        });

        modelBuilder.Entity<TbTownship>(entity =>
        {
            entity.HasKey(e => e.TownshipPkid);

            entity.ToTable("TB_Township");

            entity.Property(e => e.DistrictCode).HasMaxLength(5);
            entity.Property(e => e.StateDivisionId)
                .HasMaxLength(5)
                .HasColumnName("StateDivisionID");
            entity.Property(e => e.TownshipCode).HasMaxLength(15);
            entity.Property(e => e.TownshipName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbTownship1>(entity =>
        {
            entity.HasKey(e => e.TownshipPkid);

            entity.ToTable("TB_Township1");

            entity.Property(e => e.DistrictCode).HasMaxLength(5);
            entity.Property(e => e.StateDivisionId)
                .HasMaxLength(5)
                .HasColumnName("StateDivisionID");
            entity.Property(e => e.TownshipCode).HasMaxLength(15);
            entity.Property(e => e.TownshipName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbTownshipBankAccount>(entity =>
        {
            entity.HasKey(e => e.BankAccountPkid);

            entity.ToTable("TB_TownshipBankAccount");

            entity.Property(e => e.BankAccountId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BankAccountID");
            entity.Property(e => e.BankAccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankName).HasMaxLength(200);
            entity.Property(e => e.BankTownshipId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BankTownshipID");
            entity.Property(e => e.TownshipId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TownshipID");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.UserPkid);

            entity.ToTable("TB_User");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DepartmentType).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(300);
            entity.Property(e => e.UserDisplayName).HasMaxLength(50);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("UserID");
            entity.Property(e => e.UserRole).HasMaxLength(50);
        });

        modelBuilder.Entity<TbUserAccount>(entity =>
        {
            entity.HasKey(e => e.UserPkid);

            entity.ToTable("TB_UserAccount");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(70);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Otpcode)
                .HasMaxLength(10)
                .HasColumnName("OTPCode");
            entity.Property(e => e.Otpexpired)
                .HasColumnType("datetime")
                .HasColumnName("OTPExpired");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserRole).HasMaxLength(50);
            entity.Property(e => e.UserType).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
