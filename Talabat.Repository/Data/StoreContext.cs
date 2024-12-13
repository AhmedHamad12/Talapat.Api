using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Models.Order_Aggreate;
using talabat.Modeles.Models;
using Talabat.Repository.Data.Configrations;

namespace Talabat.Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> option):base(option) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ممكن نكرر دي لكل كلاس اتكريت 
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //او ببساطة نستحدم الامر ده
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           // base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType>  productTypes { get; set; }
        public DbSet<Order>  Orders { get; set; }
        public DbSet<DeliveryMethod>  DeliveryMethods { get; set; }
        public DbSet<OrderItem>  OrderItems { get; set; }

    }
}
