using CCMS.Patients;
using CCMS.Sessions;
using CCMS.Staffing;
using CCMS.Stores;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace CCMS.EntityFrameworkCore;

public static class CCMSDbContextModelCreatingExtensions
{
    public static void ConfigureCCMS(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Patient>(b =>
        {
            b.ToTable("Patients");
            b.HasKey(x => x.Id);
            b.Property(x => x.Code).HasMaxLength(32).IsRequired();
            b.Property(x => x.FullName).HasMaxLength(128).IsRequired();
            b.Property(x => x.Phone).HasMaxLength(32).IsRequired();
        });

        builder.Entity<Staff>(b =>
        {
            b.ToTable("Staff");
            b.HasKey(x => x.Id);
            b.Property(x => x.FullName).HasMaxLength(128).IsRequired();
            b.HasOne(x => x.DoctorProfile).WithOne().HasForeignKey<Staff>(x => x.DoctorProfileId);
        });

        builder.Entity<DoctorProfile>(b =>
        {
            b.ToTable("DoctorProfiles");
            b.HasKey(x => x.Id);
        });

        builder.Entity<Session>(b =>
        {
            b.ToTable("Sessions");
            b.HasKey(x => x.Id);
            b.HasMany<SessionMaterial>().WithOne().HasForeignKey(x => x.SessionId);
        });

        builder.Entity<SessionMaterial>(b =>
        {
            b.ToTable("SessionMaterials");
            b.HasKey(x => x.Id);
        });

        builder.Entity<Store>(b =>
        {
            b.ToTable("Stores");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.Location).HasMaxLength(128);
            b.Property(x => x.Description).HasMaxLength(1024);
            b.HasMany<RawMaterial>().WithOne().HasForeignKey(x => x.StoreId);
        });

        builder.Entity<RawMaterial>(b =>
        {
            b.ToTable("RawMaterials");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(128).IsRequired();
            b.Property(x => x.SKU).HasMaxLength(64).IsRequired();
            b.Property(x => x.Unit).IsRequired().HasMaxLength(32);
            b.Property(x => x.SupplierName).HasMaxLength(128);
            b.Property(x => x.Price).HasColumnType("decimal(18,2)");
            b.HasOne(x => x.Store).WithMany().HasForeignKey(x => x.StoreId);

        });
       

     
    }
}
