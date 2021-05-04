using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class VictimMappingConfiguration : IEntityTypeConfiguration<Victim>
    {
        public void Configure(EntityTypeBuilder<Victim> builder)
        {
            builder.Property("Victims");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.LastName).IsRequired().HasMaxLength(128);
            builder.Property(b => b.FirstName).HasMaxLength(128);
            builder.Property(b => b.MiddleName).HasMaxLength(128);
            builder.Property(b => b.IsHeroSoviet).IsRequired();
            builder.Property(b => b.IsPartisan).IsRequired();
            builder.Property(b => b.DateOfBirth).HasMaxLength(64);
            builder.Property(b => b.DateOfDeath).HasMaxLength(64);

            builder.HasOne(b => b.TypeVictim).WithMany();
            builder.HasOne(b => b.DutyStation).WithMany();
            builder.HasOne(b => b.BirthPlace).WithMany();
            builder.HasOne(b => b.ConscriptionPlace).WithMany();
            builder.HasOne(b => b.Burial).WithMany();

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
