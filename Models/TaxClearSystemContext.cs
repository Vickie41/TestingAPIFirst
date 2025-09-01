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

        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
