using System;
using System.Collections.Generic;
using FirstTestingAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstTestingAPI.Data;

public partial class LoanDbContext : DbContext
{
    public LoanDbContext()
    {
    }

    public LoanDbContext(DbContextOptions<LoanDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbLoanCollateral> TbLoanCollaterals { get; set; }

    public virtual DbSet<TbLoanMaster> TbLoanMasters { get; set; }

    public virtual DbSet<TbLoanReturnTransactionDetail> TbLoanReturnTransactionDetails { get; set; }

    public virtual DbSet<TbPersonalInformation> TbPersonalInformations { get; set; }

    public virtual DbSet<VwCreditReport> VwCreditReports { get; set; }

    public virtual DbSet<VwLoanDetail> VwLoanDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=CreditBureauTesting; Trusted_Connection=true; Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbLoanCollateral>(entity =>
        {
            entity.HasKey(e => e.CollateralId).HasName("PK__TbLoanCo__BB1A1FDCC3A576C2");

            entity.ToTable("TbLoanCollateral");

            entity.HasIndex(e => e.LoanId, "IX_TbLoanCollateral_LoanId");

            entity.HasIndex(e => e.CollateralReference, "IX_TbLoanCollateral_Reference").IsUnique();

            entity.Property(e => e.CollateralReference).HasMaxLength(50);
            entity.Property(e => e.CollateralType).HasMaxLength(10);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ForceSaleValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FullAddress).HasMaxLength(500);
            entity.Property(e => e.MarketValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TownshipCode).HasMaxLength(20);

            entity.HasOne(d => d.Loan).WithMany(p => p.TbLoanCollaterals)
                .HasForeignKey(d => d.LoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbLoanCollateral_TbLoanMaster");
        });

        modelBuilder.Entity<TbLoanMaster>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__TbLoanMa__4F5AD457DFDC09AF");

            entity.ToTable("TbLoanMaster");

            entity.HasIndex(e => e.DisbursedDate, "IX_TbLoanMaster_DisbursedDate");

            entity.HasIndex(e => e.ExpiredDate, "IX_TbLoanMaster_ExpiredDate");

            entity.HasIndex(e => e.OrganizationLoanId, "IX_TbLoanMaster_OrganizationLoanID").IsUnique();

            entity.HasIndex(e => e.PersonPkid, "IX_TbLoanMaster_PersonPkid");

            entity.HasIndex(e => e.SeparateAccountNo, "IX_TbLoanMaster_SeparateAccountNo").IsUnique();

            entity.Property(e => e.AccountTypeCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.ApplicantTypeCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.BranchName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DisbursedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IndustrialSectorCode).HasMaxLength(10);
            entity.Property(e => e.InterestInstalmentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InterestOutstandingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InterestOverdueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InterestPaymentFrequency)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.InterestRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LastPaymentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LastPaymentFor)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.OrganizationLoanId)
                .HasMaxLength(50)
                .HasColumnName("OrganizationLoanID");
            entity.Property(e => e.PrincipalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrincipalInstalmentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrincipalOutstandingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrincipalOverdueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrincipalPaymentFrequency)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.ProductStatusCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.ProductTypeCode).HasMaxLength(10);
            entity.Property(e => e.SeparateAccountNo).HasMaxLength(50);
            entity.Property(e => e.SmeFlag).HasColumnName("SME_Flag");

            entity.HasOne(d => d.PersonPk).WithMany(p => p.TbLoanMasters)
                .HasForeignKey(d => d.PersonPkid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbLoanMaster_TbPersonalInformation");
        });

        modelBuilder.Entity<TbLoanReturnTransactionDetail>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__TbLoanRe__55433A6B26B4AEC2");

            entity.ToTable("TbLoanReturnTransactionDetail");

            entity.HasIndex(e => e.LoanId, "IX_TbLoanReturnTransactionDetail_LoanId");

            entity.HasIndex(e => e.McisrepaymentNumber, "IX_TbLoanReturnTransactionDetail_MCISRepayment").IsUnique();

            entity.HasIndex(e => e.OrganizationRepaymentId, "IX_TbLoanReturnTransactionDetail_OrgRepaymentId").IsUnique();

            entity.HasIndex(e => e.RepaymentDate, "IX_TbLoanReturnTransactionDetail_RepaymentDate");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.McisaccountNumber)
                .HasMaxLength(100)
                .HasColumnName("MCISAccountNumber");
            entity.Property(e => e.McisrepaymentNumber)
                .HasMaxLength(100)
                .HasColumnName("MCISRepaymentNumber");
            entity.Property(e => e.OrganizationRepaymentId).HasMaxLength(50);
            entity.Property(e => e.RepaymentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RepaymentFor)
                .HasMaxLength(1)
                .IsFixedLength();

            entity.HasOne(d => d.Loan).WithMany(p => p.TbLoanReturnTransactionDetails)
                .HasForeignKey(d => d.LoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbLoanReturnTransactionDetail_TbLoanMaster");
        });

        modelBuilder.Entity<TbPersonalInformation>(entity =>
        {
            entity.HasKey(e => e.PersonPkid).HasName("PK__TbPerson__D6402275EA00C45B");

            entity.ToTable("TbPersonalInformation");

            entity.HasIndex(e => e.CreatedDate, "IX_TbPersonalInformation_CreatedDate");

            entity.HasIndex(e => e.IsActive, "IX_TbPersonalInformation_IsActive");

            entity.HasIndex(e => new { e.Nrcregion, e.Nidtype, e.Nrcnumber }, "IX_TbPersonalInformation_NRC").IsUnique();

            entity.HasIndex(e => e.Phone, "IX_TbPersonalInformation_Phone").IsUnique();

            entity.Property(e => e.AccountNumber).HasMaxLength(100);
            entity.Property(e => e.AccountType).HasMaxLength(50);
            entity.Property(e => e.AddressRemark).HasMaxLength(200);
            entity.Property(e => e.AddressTypeCode).HasMaxLength(10);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FatherNameEnglish).HasMaxLength(100);
            entity.Property(e => e.FatherNameMm)
                .HasMaxLength(100)
                .HasColumnName("FatherNameMM");
            entity.Property(e => e.FullAddress).HasMaxLength(500);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.IdcexpireAt).HasColumnName("IDCExpireAt");
            entity.Property(e => e.Idcnumber)
                .HasMaxLength(50)
                .HasColumnName("IDCNumber");
            entity.Property(e => e.Idtype)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IDType");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsMainPerson).HasDefaultValue(false);
            entity.Property(e => e.IsRecordEdited).HasDefaultValue(false);
            entity.Property(e => e.Jicanumber).HasMaxLength(100);
            entity.Property(e => e.Marital)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.NameEnglish).HasMaxLength(100);
            entity.Property(e => e.NameMm)
                .HasMaxLength(100)
                .HasColumnName("NameMM");
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.Nidtype)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("NIDType");
            entity.Property(e => e.Nrcnumber)
                .HasMaxLength(20)
                .HasColumnName("NRCNumber");
            entity.Property(e => e.Nrcregion)
                .HasMaxLength(50)
                .HasColumnName("NRCRegion");
            entity.Property(e => e.Occupation).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.Race).HasMaxLength(50);
            entity.Property(e => e.RegionId).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate).HasMaxLength(50);
            entity.Property(e => e.SpouseIdcardExpireAt).HasColumnName("SpouseIDCard_ExpireAt");
            entity.Property(e => e.SpouseIdcardNo)
                .HasMaxLength(50)
                .HasColumnName("SpouseIDCard_No");
            entity.Property(e => e.SpouseIdtype)
                .HasMaxLength(10)
                .HasColumnName("SpouseIDType");
            entity.Property(e => e.SpouseNameEng).HasMaxLength(100);
            entity.Property(e => e.SpouseNameMm)
                .HasMaxLength(100)
                .HasColumnName("SpouseNameMM");
            entity.Property(e => e.SpouseNidtype)
                .HasMaxLength(20)
                .HasColumnName("SpouseNIDType");
            entity.Property(e => e.SpouseNrcNo)
                .HasMaxLength(50)
                .HasColumnName("SpouseNRC_No");
            entity.Property(e => e.SpouseNrcRegion)
                .HasMaxLength(10)
                .HasColumnName("SpouseNRC_Region");
            entity.Property(e => e.StateDivisionId).HasMaxLength(50);
            entity.Property(e => e.StateOrRegionCode).HasMaxLength(20);
            entity.Property(e => e.TownshipCode).HasMaxLength(20);
            entity.Property(e => e.TownshipId).HasMaxLength(50);
            entity.Property(e => e.TransactionId).HasMaxLength(100);
        });

        modelBuilder.Entity<VwCreditReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_CreditReports");

            entity.Property(e => e.FullNrc)
                .HasMaxLength(73)
                .HasColumnName("FullNRC");
            entity.Property(e => e.NameEnglish).HasMaxLength(100);
            entity.Property(e => e.TotalOutstanding).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VwLoanDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_LoanDetails");

            entity.Property(e => e.BranchName).HasMaxLength(100);
            entity.Property(e => e.DisbursedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FatherNameEnglish).HasMaxLength(100);
            entity.Property(e => e.FatherNameMm)
                .HasMaxLength(100)
                .HasColumnName("FatherNameMM");
            entity.Property(e => e.FullNrc)
                .HasMaxLength(73)
                .HasColumnName("FullNRC");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.InterestOutstandingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InterestOverdueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InterestRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Marital)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.NameEnglish).HasMaxLength(100);
            entity.Property(e => e.NameMm)
                .HasMaxLength(100)
                .HasColumnName("NameMM");
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.OrganizationLoanId)
                .HasMaxLength(50)
                .HasColumnName("OrganizationLoanID");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PrincipalOutstandingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrincipalOverdueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductStatusCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.ProductTypeCode).HasMaxLength(10);
            entity.Property(e => e.Race).HasMaxLength(50);
            entity.Property(e => e.SeparateAccountNo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
