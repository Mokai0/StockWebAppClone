using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StockWebApp.Data;
using StockWebApp.Models;

namespace StockWebApp.Controllers
{
    public class ProductsController : Controller
    {
        //Both of these capture instances of the Context class but considering the circumstance I'm more comfortable with using one vs the other in certain situations
        private Context db = new Context();
        static Context GetContext()
        {
            var context = new Context();
            return context;
        }

        //Get: This will get a specific product based on it's Product.Id property
        public static Product GetProducts(int selected)
        {
            using (Context context = GetContext())
            {
                return context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Where(p => p.Id == selected)
                    .SingleOrDefault();
            }
        }

        // Get: This will initiate a call to generate the "landing page" or "home page" of this app in an organized manner
        public ActionResult Index()
        {
            using (Context context = GetContext())
            {
                var listOfProducts = context.Products
                            .Include(p => p.Brand)
                            .Include(p => p.Category)
                            .OrderBy(p => p.Brand.Name)
                            .ThenBy(p => p.Category.Ref)
                            .ThenBy(p => p.ProductName)
                            .ToList();
                return View(listOfProducts); 
            }
        }

        //Get: This will reorganize the products displayed in the Index View by their respective Brand Names
        public ActionResult IndexBrands()
        {
            using (Context context = GetContext())
            {
                var listOfProducts = context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .OrderBy(p => p.Brand.Name)
                    .ToList();
                return View(listOfProducts);
            }
        }

        //Get: This will reorganize the products displayed in the Index View by their respective Categories
        public ActionResult IndexCategories()
        {
            using (Context context = GetContext())
            {
                var listOfProducts = context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .OrderBy(p => p.Category.Info)
                    .ToList();
                return View(listOfProducts);
            }
        }

        //Get: This will reorganize the products displayed in the Index View by their respective Product Names
        public ActionResult IndexNames()
        {
            using (Context context = GetContext())
            {
                var listOfProducts = context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .OrderBy(p => p.ProductName)
                    .ToList();
                return View(listOfProducts);
            }
        }

        //Get: This will reorganize the products displayed in the Index View by their respective Quantities in stock
        public ActionResult IndexQuantities()
        {
            using (Context context = GetContext())
            {
                var listOfProducts = context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .OrderBy(p => p.Quantity)
                    .ToList();
                return View(listOfProducts);
            }
        }

        //Get: This will reorganize the products displayed in the Index View to only show the products with the ExpirationDate property and then organize them
        public ActionResult IndexExpirations()
        {
            using (Context context = GetContext())
            {
                var listOfProducts = context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Where(p => p.ExpirationDate != null)
                    .OrderBy(p => p.ExpirationDate)
                    .ThenBy(p => p.Quantity)
                    .ToList();
                return View(listOfProducts);
            }
        }

        // Get: This will initiate a call to retrieve and render the Create View giving the user the opportunity to add a product to the database
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Info");
            //This won't work - I need something else to prevent duplicates.
            //ViewBag.Product.ProductName = new SelectList(db.Products, "Id", "ProductName");
            return View();
        }

        // Post: This will send the information obtained from the user to create a product in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrandId,CategoryId,ProductName,Quantity,ExpirationDate,MyProperty")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Exception)
                {
                    ModelState.AddModelError(null, "That product already exists");
                    ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
                    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Info", product.CategoryId);
                    return View(product);
                }
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Info", product.CategoryId);
            return View(product);
        }

        // Get: This will initiate a call to retrieve and render the Edit View allowing the user the chance to update the information on the product that they have selected - which was retrieved by the product's Product.Id property
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Info", product.CategoryId);
            return View(product);
        }

        // Post: This will send the updated information to the database modifying the existing item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrandId,CategoryId,ProductName,Quantity,ExpirationDate,MyProperty")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Info", product.CategoryId);
            return View(product);
        }

        // Get: This will initiate a call to retrieve and render the Delete View populating it with the information available about the item, which was selected by it's Product.Id property.
        public ActionResult Delete(int id)
        {
            using (Context context = GetContext())
            {
                var product = GetProducts(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
        }

        // Post: After recieving validation from the user a call will be sent to the database to delete the selected product and all of its attatched values
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
