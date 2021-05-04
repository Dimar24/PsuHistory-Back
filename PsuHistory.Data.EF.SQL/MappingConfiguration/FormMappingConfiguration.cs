using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsuHistory.Data.Domain.Models.Histories;

namespace PsuHistory.Data.EF.SQL.MappingConfiguration
{
    class FormMappingConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.Property("Forms");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.LastName).IsRequired().HasMaxLength(128);
            builder.Property(b => b.FirstName).HasMaxLength(128);
            builder.Property(b => b.MiddleName).HasMaxLength(128);

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt).IsRequired();
        }
    }
}
