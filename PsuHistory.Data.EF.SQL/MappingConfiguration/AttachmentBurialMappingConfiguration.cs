using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class AttachmentBurialMappingConfiguration : IEntityTypeConfiguration<AttachmentBurial>
    {
        public void Configure(EntityTypeBuilder<AttachmentBurial> builder)
        {
            builder.Property("AttachmentBurials");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.FileName).IsRequired().HasMaxLength(64);
            builder.Property(b => b.FilePath).IsRequired().HasMaxLength(256);
            builder.Property(b => b.FileType).IsRequired().HasMaxLength(8);

            builder.HasOne(b => b.Burial).WithMany();

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
