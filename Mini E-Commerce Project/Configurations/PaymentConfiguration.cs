using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(py => py.Id);
            builder.HasIndex(py => py.Id);
            builder.Property(py => py.Amount).IsRequired();
            builder.Property(py => py.PaymentDate).IsRequired();
            builder.HasOne<Product>().WithMany().HasForeignKey(py => py.OrderId);

        }
    }
}
