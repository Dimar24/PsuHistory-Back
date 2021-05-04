using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Histories;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class AttachmentFormMappingConfiguration : IEntityTypeConfiguration<AttachmentForm>
    {
        public void Configure(EntityTypeBuilder<AttachmentForm> builder)
        {
            builder.Property("AttachmentForms");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.FileName).IsRequired().HasMaxLength(64);
            builder.Property(b => b.FilePath).IsRequired().HasMaxLength(256);
            builder.Property(b => b.FileType).IsRequired().HasMaxLength(8);

            builder.HasOne(b => b.Form).WithMany();

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
