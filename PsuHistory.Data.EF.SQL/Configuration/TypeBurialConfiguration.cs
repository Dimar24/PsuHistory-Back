using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class TypeBurialConfiguration : IEntityTypeConfiguration<TypeBurial>
    {
        public void Configure(EntityTypeBuilder<TypeBurial> builder)
        {
            builder.ToTable("TypeBurials").HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired().HasMaxLength(128);

            builder.Property(b => b.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.UpdatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
