using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class TypeVictimConfiguration : IEntityTypeConfiguration<TypeVictim>
    {
        public void Configure(EntityTypeBuilder<TypeVictim> builder)
        {
            builder.ToTable("TypeVictims").HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired().HasMaxLength(128);

            builder.Property(b => b.CreatedAt);
            builder.Property(b => b.UpdatedAt);
        }
    }
}
