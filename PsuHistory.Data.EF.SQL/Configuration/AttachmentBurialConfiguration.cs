using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;
using System;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class AttachmentBurialConfiguration : IEntityTypeConfiguration<AttachmentBurial>
    {
        public void Configure(EntityTypeBuilder<AttachmentBurial> builder)
        {
            builder.ToTable("AttachmentBurials").HasKey(b => b.Id);

            builder.Property(b => b.FileName).IsRequired().HasMaxLength(64);
            builder.Property(b => b.FilePath).IsRequired().HasMaxLength(256);
            builder.Property(b => b.FileType).IsRequired().HasMaxLength(8);

            builder.HasOne(b => b.Burial).WithMany(b => b.AttachmentBurials).HasForeignKey(b => b.BurialId);

            builder.Property(b => b.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.UpdatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
