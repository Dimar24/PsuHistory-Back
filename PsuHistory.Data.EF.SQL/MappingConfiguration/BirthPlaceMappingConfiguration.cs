using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class BirthPlaceMappingConfiguration : IEntityTypeConfiguration<BirthPlace>
    {
        public void Configure(EntityTypeBuilder<BirthPlace> builder)
        {
            builder.Property("BirthPlaces");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Place).IsRequired().HasMaxLength(512);

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
