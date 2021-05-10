using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class ConscriptionPlaceConfiguration : IEntityTypeConfiguration<ConscriptionPlace>
    {
        public void Configure(EntityTypeBuilder<ConscriptionPlace> builder)
        {
            builder.ToTable("ConscriptionPlaces").HasKey(b => b.Id);

            builder.Property(b => b.Place).IsRequired().HasMaxLength(512);

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
