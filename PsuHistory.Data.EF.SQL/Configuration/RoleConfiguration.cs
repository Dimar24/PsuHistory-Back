using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Users;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles").HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired().HasMaxLength(64);

            builder.Property(b => b.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.UpdatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
