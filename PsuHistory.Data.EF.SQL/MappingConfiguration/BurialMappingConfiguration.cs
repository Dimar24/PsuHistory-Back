using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class BurialMappingConfiguration : IEntityTypeConfiguration<Burial>
    {
        public void Configure(EntityTypeBuilder<Burial> builder)
        {
            builder.Property("Burials");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.NumberBurial).IsRequired();
            builder.Property(b => b.Location).IsRequired().HasMaxLength(512);
            builder.Property(b => b.NumberPeople).IsRequired();
            builder.Property(b => b.UnknownNumber).IsRequired();
            builder.Property(b => b.Year).IsRequired();
            builder.Property(b => b.Latitude).IsRequired();
            builder.Property(b => b.Longitude).IsRequired();
            builder.Property(b => b.Description).IsRequired().HasMaxLength(4096);

            builder.HasOne(b => b.TypeBurial).WithMany();

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
