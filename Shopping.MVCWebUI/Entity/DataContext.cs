using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Entity
{
    public class DataContext : DbContext
    {
        public DataContext() : base("dbShoppingConn")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

       

    }
}