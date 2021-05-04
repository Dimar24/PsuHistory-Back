using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class ConscriptionPlaceMappingConfiguration : IEntityTypeConfiguration<ConscriptionPlace>
    {
        public void Configure(EntityTypeBuilder<ConscriptionPlace> builder)
        {
            builder.Property("ConscriptionPlaces");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Place).IsRequired().HasMaxLength(512);

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
