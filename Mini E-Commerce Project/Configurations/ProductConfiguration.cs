using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(pr => pr.Id);
            builder.HasIndex(pr => pr.Id);
            builder.Property(pr => pr.Name).IsRequired(true).HasMaxLength(60);
            builder.Property(pr => pr.Price).IsRequired(true).HasColumnType("decimal(8,2)");
            builder.Property(pr => pr.Stock).IsRequired(true);
            builder.Property(pr => pr.Description).IsRequired(true).HasMaxLength(200);
            builder.Property(pr => pr.CreatedDate).IsRequired(true);
            builder.Property(pr => pr.UpdatedDate).IsRequired(true);

            builder.HasCheckConstraint("CK_Price", "Price > 0");
            builder.HasCheckConstraint("CK_StockCount", "StockCount >= 0");

        }
    }
}
