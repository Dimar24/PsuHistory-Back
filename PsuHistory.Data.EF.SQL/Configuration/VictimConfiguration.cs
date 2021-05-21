using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class VictimConfiguration : IEntityTypeConfiguration<Victim>
    {
        public void Configure(EntityTypeBuilder<Victim> builder)
        {
            builder.ToTable("Victims").HasKey(b => b.Id);

            builder.Property(b => b.LastName).IsRequired().HasMaxLength(128);
            builder.Property(b => b.FirstName).HasMaxLength(128);
            builder.Property(b => b.MiddleName).HasMaxLength(128);
            builder.Property(b => b.IsHeroSoviet).IsRequired();
            builder.Property(b => b.IsPartisan).IsRequired();
            builder.Property(b => b.DateOfBirth).HasMaxLength(64);
            builder.Property(b => b.DateOfDeath).HasMaxLength(64);

            builder.HasOne(b => b.TypeVictim).WithMany().HasForeignKey(b => b.TypeVictimId);
            builder.HasOne(b => b.DutyStation).WithMany().HasForeignKey(b => b.DutyStationId);
            builder.HasOne(b => b.BirthPlace).WithMany().HasForeignKey(b => b.BirthPlaceId);
            builder.HasOne(b => b.ConscriptionPlace).WithMany().HasForeignKey(b => b.ConscriptionPlaceId);
            builder.HasOne(b => b.Burial).WithMany().HasForeignKey(b => b.BurialId);
            builder.HasOne(b => b.Burial).WithMany().HasForeignKey(b => b.BurialId);

            builder.Property(b => b.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.UpdatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
