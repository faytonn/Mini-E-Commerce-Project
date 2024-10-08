﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mini_E_Commerce_Project.Enums;
using Mini_E_Commerce_Project.Models;

namespace Mini_E_Commerce_Project.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.Id);
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotalAmount).IsRequired();
            builder.Property(o => o.Status).IsRequired().HasDefaultValue(StatusEnum.Pending);
            builder.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId);
            builder.HasMany(o => o.OrderDetails).WithOne(od => od.Order).HasForeignKey(od => od.OrderId);

            builder.HasCheckConstraint("CK_TotalAmount", "TotalAmount >= 0");
        }
    }
}
