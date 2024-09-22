using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using teste_atak.Domain.Entities;

namespace teste_atak.Infra.Data.Schemas
{
    internal class CustomerSchema : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(256);

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.ToTable("customers");
        }
    }
}
