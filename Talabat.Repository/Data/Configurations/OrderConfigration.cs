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
    internal class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           // throw new NotImplementedException();
            builder.Property(p=>p.Status)
                   .HasConversion(OStats=>OStats.ToString(),OStatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),OStatus));
            builder.Property(p => p.SubTotal)
                    .HasColumnType("decimal(18,2)");
            builder.OwnsOne(p => p.ShippingAddress, SA => SA.WithOwner());
            builder.HasOne(p=>p.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
