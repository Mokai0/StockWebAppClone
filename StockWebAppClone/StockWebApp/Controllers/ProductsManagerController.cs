using StockWebApp.Data;
using StockWebApp.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace StockWebApp.Controllers
{
    public class ProductsManagerController : Controller
    {
        static Context GetContext()
        {
            var c = new Context();
            return c;
        }

        //Get: Selects a single product by its Id
        public static Product GetProduct(int selectedId)
        {
            using (Context c = GetContext())
            {
                return c.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Where(p => p.Id == selectedId)
                    .SingleOrDefault();
            }
        }

        // Get: All products in an ordered list
        public ActionResult Index()
        {
            using (Context c = GetContext())
            {
                var listOfProducts = c.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .OrderBy(p => p.Brand.Name)
                    .ThenBy(p => p.Category.Ref)
                    .ThenBy(p => p.ProductName)
                    .ToList();
                return View(listOfProducts);
            }
        }

        //Get: All products in a list orderd by Brand Name
        //Get
    }
}