using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class BirthPlaceConfiguration : IEntityTypeConfiguration<BirthPlace>
    {
        public void Configure(EntityTypeBuilder<BirthPlace> builder)
        {
            builder.ToTable("BirthPlaces").HasKey(b => b.Id);

            builder.Property(b => b.Place).IsRequired().HasMaxLength(512);

            builder.Property(b => b.CreatedAt);
            builder.Property(b => b.UpdatedAt);
        }
    }
}
