using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using teste_atak.Domain.Entities;

namespace teste_atak.Infra.Data.Schemas
{
    internal class UserSchema : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256); 

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.AvatarUrl)
                .HasMaxLength(256);

            builder.Property(u => u.IsEmailVerified)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.ToTable("users");
        }
    }
}
