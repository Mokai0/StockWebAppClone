using StockWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StockWebApp.Data
{
    //This will seed the test data into the database.
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
            var CaliforniaGarden = new Brand() { Name = "California Garden" };
            var Cedar = new Brand() { Name = "Cedar" };
            var Lipton = new Brand() { Name = "Lipton Tea" };
            var Ahmed = new Brand() { Name = "Ahmad Tea" };
            var Nescafe = new Brand() { Name = "Nescafe" };
            var Nido = new Brand() { Name = "Nido" };
            var Puck = new Brand() { Name = "Puck" };

            //Ask if it's possible or best practice to initialize the catagories within the Category.cs Model
            var Dry = new Category() { Info = "Dried Foods", Ref = "Dry" };
            var Can = new Category() { Info = "Canned Foods", Ref = "Can" };
            var Dairy = new Category() { Info = "Dairy Foods", Ref = "Dairy" };
            var Spice = new Category() { Info = "Spices", Ref = "Spice" };
            var Frozen = new Category() { Info = "Frozen Foods", Ref = "Frozen" };
            var Preserve = new Category() { Info = "Preserved Foods", Ref = "Preserve" };
            var Honey = new Category() { Info = "Honey", Ref = "Honey" };
            var Tea = new Category() { Info = "Tea", Ref = "Tea" };
            var Coffee = new Category() { Info = "Coffee", Ref = "Coffee" };

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
                Brand = Cortas,
                ProductName = "Hummus",
                Quantity = 12,
                Category = Can,
                ExpirationDate = new DateTime(2020, 8, 1)
            });
            context.Products.Add(new Product()
            {
                Brand = Tazah,
                ProductName = "Fava Beans",
                Quantity = 1,
                Category = Dry,
                ExpirationDate = new DateTime(2023, 5, 1)
            });
            context.Products.Add(new Product()
            {
                Brand = Shan,
                ProductName = "Biryani",
                //While this is a Decimal it needed a 'M' Suffix to populate correctly
                Quantity = 2.5M,
                Category = Spice,
                ExpirationDate = new DateTime(2023, 1, 1)
            });
            context.Products.Add(new Product()
            {
                Brand = CaliforniaGarden,
                ProductName = "Hummus Masabeha",
                Quantity = 2,
                Category = Can,
                ExpirationDate = new DateTime(2022, 1, 1)
            });
            context.Products.Add(new Product()
            {
                Brand = Cedar,
                ProductName = "Strawberry Jam",
                Quantity = 2,
                Category = Preserve
            });
            context.Products.Add(new Product()
            {
                Brand = Lipton,
                ProductName = "Tea Bags",
                Quantity = 1.5M,
                Category = Tea
            });
            context.Products.Add(new Product()
            {
                Brand = Ahmed,
                ProductName = "Special Blend Loose",
                Quantity = 4,
                Category = Tea
            });
            context.Products.Add(new Product()
            {
                Brand = Nescafe,
                ProductName = "3 in 1 Coffee",
                Quantity = 2,
                Category = Coffee
            });
            context.Products.Add(new Product()
            {
                Brand = Nido,
                ProductName = "Dried Milk 128oz",
                Quantity = 1,
                Category = Dairy
            });
            context.Products.Add(new Product()
            {
                Brand = Puck,
                ProductName = "Cream Cheese 900g",
                Quantity = 3,
                Category = Dairy
            });
            context.SaveChanges();
            context.Products.Add(new Product()
            {
                Brand = Ziyad,
                ProductName = "Honey with Comb",
                Quantity = 1,
                Category = Honey
            });
            context.SaveChanges();
        }
    }
}