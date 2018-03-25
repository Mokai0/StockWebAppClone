using StockWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StockWebApp.Data
{
    //This class should handle the various CRUD operations handling.
    public static class Repository
    {
        //<summary> Private method that returns a db context to the class.
        //<returns> An instance of the context class.
        static Context GetContext()
        {
            var context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
            return context;
        }

        //<summary> Returns a count of the products.
        //<returns> An integar count of the products.
        public static int GetProductCount()
        {
            using (Context context = GetContext())
            {
                return context.Products.Count();
            }
        }

        //<summary> Returns a list of products ordered by Brand (as this would correlate to what distributer I'd need to consult) then by Category (because this way I could go through each section of the store for the respective distributer without the need for doubling back).
        //<returns> An IList collection of Product entity instances.
        public static IList<Product> GetProducts()
        {
            using (Context context = GetContext())
            {
                return context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .OrderBy(p => p.Brand.Name)
                    .ThenBy(p => p.Category.Ref)
                    .ThenBy(p => p.ProductName)
                    .ToList();
            }
        }

        //<summary> Pull a single product.
        //<returns> A fully populated Product entity instance.
        ///<param name="productId"> The specific product I wanna get.
        public static Product GetProduct(int productId)
        {
            using (Context context = GetContext())
            {
                return context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Where(p => p.Id == productId)
                    .SingleOrDefault();
            }
        }

        //<summary> Returns a list of Brands ordered by name.
        //<returns> An IList collection of Brand entity instances.
        public static IList<Brand> GetBrands()
        {
            //IList<Brand> above is referencing the Brand class,
            //context.Brands however is referencing the DbSet property within the Context class.
            using (Context context = GetContext())
            {
                return context.Brands
                    .OrderBy(b => b.Name)
                    .ToList();
            }
        }

        //<summary> Returns a single Brand.
        //<returns> A Brand entity instance.
        ///<param name="brandId"> The specific Brand I want returned.
        public static Brand GetBrand(int brandId)
        {
            using (Context context = GetContext())
            {
                return context.Brands
                    .Where(b => b.Id == brandId)
                    .SingleOrDefault();
            }
        }

        //<summary> Return a list of Categories ordered by name.
        //<returns> An IList collection of Category entity instances.
        public static IList<Category> GetCategories()
        {
            using (Context context = GetContext())
            {
                return context.Categories
                    .OrderBy(b => b.Info)
                    .ToList();
                //I chose to retrieve the Categories here by Info instead of Ref for 2 reasons:
                //The first is for readability
                //The second is because Ref is more for my own (back end and programming) purposes.
            }
        }

        //<summary> Returns a single Category.
        //<returns> A Category entity instance.
        ///<param name="categoryId"> The specific Category I want returned.
        public static Category GetCategory(int categoryId)
        {
            using (Context context = GetContext())
            {
                return context.Categories
                    .Where(c => c.Id == categoryId)
                    .SingleOrDefault();
            }
        }

        //<summary> Adds a product.
        //<returns> Nothing.
        ///<param name="product"> The Product entity instance to add.
        public static void AddProduct(Product product)
        {
            using (Context context = GetContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        //<summary> Updates a product.
        //<returns> Nothing.
        ///<param name="product"> The Product entity instance to update.
        public static void UpdateProduct(Product product)
        {
            //TODO
        }

        //<summary> Deletes a product.
        //<returns> Nothing.
        ///<param name="productId"> The product Id to delete.
        public static void DeleteProduct(int productId)
        {
            //TODO
        }
    }
}