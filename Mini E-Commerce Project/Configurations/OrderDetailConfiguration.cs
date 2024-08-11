using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.Id);
            builder.HasIndex(od => od.Id);
            builder.Property(od => od.Quantity).IsRequired();
            builder.Property(od => od.PricePerItem).IsRequired();
            builder.
        }
    }
}
