using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class BurialConfiguration : IEntityTypeConfiguration<Burial>
    {
        public void Configure(EntityTypeBuilder<Burial> builder)
        {
            builder.ToTable("Burials").HasKey(b => b.Id);

            builder.Property(b => b.NumberBurial).IsRequired();
            builder.Property(b => b.Location).IsRequired().HasMaxLength(512);
            builder.Property(b => b.KnownNumber).IsRequired();
            builder.Property(b => b.UnknownNumber).IsRequired();
            builder.Property(b => b.Year).IsRequired();
            builder.Property(b => b.Latitude).IsRequired();
            builder.Property(b => b.Longitude).IsRequired();
            builder.Property(b => b.Description).IsRequired().HasMaxLength(4096);

            builder.HasOne(b => b.TypeBurial).WithMany().HasForeignKey(b => b.TypeBurialId);

            builder.Property(b => b.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.UpdatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
