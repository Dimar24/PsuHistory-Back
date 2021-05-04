using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class DutyStationMappingConfiguration : IEntityTypeConfiguration<DutyStation>
    {
        public void Configure(EntityTypeBuilder<DutyStation> builder)
        {
            builder.Property("DutyStations");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Place).IsRequired().HasMaxLength(512);

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
