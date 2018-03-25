using StockWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace StockWebApp.Data
{
    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        public Context()
        {
            Database.SetInitializer(new
            //DropCreateDatabaseAlways<Context>());
            //Will now be using the custom made initializer
            DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions
                .Remove<PluralizingTableNameConvention>();
            //This makes it so that the table names aren't the plural verison of the classes

            modelBuilder.Entity<Product>()
                .Property(p => p.Quantity)
                .HasPrecision(5, 2);
            //This restricts the precision and scale of the 'Quantity' so that it reads ###.##
        }
    }
}