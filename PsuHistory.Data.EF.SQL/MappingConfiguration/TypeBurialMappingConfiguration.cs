using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class TypeBurialMappingConfiguration : IEntityTypeConfiguration<TypeBurial>
    {
        public void Configure(EntityTypeBuilder<TypeBurial> builder)
        {
            builder.Property("TypeBurials");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired().HasMaxLength(128);

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
