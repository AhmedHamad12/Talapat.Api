using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Modeles.Models;

namespace Talabat.Repository.Data.Configrations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.productBrand).WithMany()
                .HasForeignKey(p => p.ProductBrandId);
                //.OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p=>p.productType).WithMany()
                .HasForeignKey(p=>p.ProductTypeId);
            builder.Property(p=>p.name).IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.description).IsRequired()
                ;
            builder.Property(p=>p.pictureUrl).IsRequired()
                ;
            builder.Property(p=>p.price).HasColumnType("decimal(18,2)");
        }
    }
}
