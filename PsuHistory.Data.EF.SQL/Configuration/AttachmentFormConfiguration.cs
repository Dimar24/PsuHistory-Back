using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Histories;

namespace PsuHistory.Data.EF.SQL.Configuration
{
    class AttachmentFormConfiguration : IEntityTypeConfiguration<AttachmentForm>
    {
        public void Configure(EntityTypeBuilder<AttachmentForm> builder)
        {
            builder.ToTable("AttachmentForms").HasKey(b => b.Id);

            builder.Property(b => b.FileName).IsRequired().HasMaxLength(64);
            builder.Property(b => b.FilePath).IsRequired().HasMaxLength(256);
            builder.Property(b => b.FileType).IsRequired().HasMaxLength(8);

            builder.HasOne(b => b.Form).WithMany(b => b.AttachmentForms).HasForeignKey(b => b.FormId);

            builder.Property(b => b.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.UpdatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
