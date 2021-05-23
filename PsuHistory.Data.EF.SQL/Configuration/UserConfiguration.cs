using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(b => b.Id);

            builder.Property(b => b.Mail).IsRequired().HasMaxLength(256);
            builder.Property(b => b.Password).IsRequired().HasMaxLength(64);

            builder.HasOne(b => b.Role).WithMany(b => b.Users).HasForeignKey(b => b.RoleId);

            builder.Property(b => b.CreatedAt);
            builder.Property(b => b.UpdatedAt);
        }
    }
}
