using StockWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StockWebApp.Data
{
    internal class DatabaseInitializer
        : DropCreateDatabaseIfModelChanges<Context>
    //: DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            var Ziyad = new Brand() { Name = "Ziyad" };
            var Cortas = new Brand() { Name = "Cortas" };
            var Tazah = new Brand() { Name = "Tazah" };
            var Shan = new Brand() { Name = "Shan" };

            //Ask if it's possible or best practice to initialize the catagories within the Category.cs Model
            var Dry = new Category() { Info = "Dried Foods", Ref = "Dry" };
            var Can = new Category() { Info = "Canned Foods", Ref = "Can" };
            var Dairy = new Category() { Info = "Dairy Foods", Ref = "Dairy" };
            var Spice = new Category() { Info = "Spices", Ref = "Spice" };
            var Frozen = new Category() { Info = "Frozen Foods", Ref = "Frozen" };

            context.Products.Add(new Product()
            {
                Brand = Ziyad,
                ProductName = "Okra Zero",
                Quantity = 5,
                Category = Frozen,
                ExpirationDate = new DateTime(2023, 1, 1)
            });
            context.Products.Add(new Product()
            {
                Brand = Ziyad,
                ProductName = "Fava Beans",
                Quantity = 3,
                Category = Dry
            });
            context.Products.Add(new Product()
            {
                Brand = Cortas,
                ProductName = "Hummus",
                Quantity = 12,
                Category = Can
            });
            context.Products.Add(new Product()
            {
                Brand = Cortas,
                ProductName = "Fava Beans",
                Quantity = 1,
                Category = Can
            });
            context.Products.Add(new Product()
            {
                Brand = Tazah,
                ProductName = "Hummus",
                Quantity = 2,
                Category = Can
            });
            context.Products.Add(new Product()
            {
                Brand = Tazah,
                ProductName = "Fava Beans",
                Quantity = 8,
                Category = Can
            });
            context.Products.Add(new Product()
            {
                Brand = Shan,
                ProductName = "Biryani",
                Quantity = 2,
                Category = Spice
            });
            context.SaveChanges();
        }
    }
}