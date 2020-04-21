using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ZC.Customer.Model.Models;

namespace ZC.Customer.Repository.Mappings
{
    public class ProductInfoMapping : IEntityTypeConfiguration<ProductInfo>
    {
        public void Configure(EntityTypeBuilder<ProductInfo> builder)
        {
            builder.ToTable("ProductInfo");
        }
    }
}
