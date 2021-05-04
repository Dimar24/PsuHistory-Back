using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class TypeVictimMappingConfiguration : IEntityTypeConfiguration<TypeVictim>
    {
        public void Configure(EntityTypeBuilder<TypeVictim> builder)
        {
            builder.Property("TypeVictims");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired().HasMaxLength(128);

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
