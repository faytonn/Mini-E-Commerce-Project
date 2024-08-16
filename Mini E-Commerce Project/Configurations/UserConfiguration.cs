using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Id);
            builder.Property(u => u.FullName).IsRequired(true).HasMaxLength(150);
            builder.Property(u => u.Email).IsRequired(true).HasMaxLength(50);
            builder.HasIndex(u =>u.Email).IsUnique();
            builder.Property(u => u.Password).IsRequired(true);
            builder.Property(u => u.Address).IsRequired(true).HasMaxLength(250);
            builder.Property(u => u.Balance).HasDefaultValue(10000);

            //builder.HasCheckConstraint("CK_Email")
        }
    }
}
