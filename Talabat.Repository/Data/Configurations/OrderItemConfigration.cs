using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Models.Order_Aggreate;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");
            builder.OwnsOne(p=> p.Product, PI => PI.WithOwner());

           //throw new NotImplementedException();
        }
    }
}
