using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockWebApp.Data;
using StockWebApp.Models;

namespace StockWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private Context db = new Context();
        static Context GetContext()
        {
            var context = new Context();
            return context;
        }

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

        // GET: Products
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
            //var products = db.Products.Include(p => p.Brand).Include(p => p.Category);
            //return View(products.ToList());
        }

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
                //var NameList = db.Products.Include(p => p.Brand).Include(p => p.Category).OrderBy(p => p.ProductName);
                //return View(NameList.ToList());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Info");
            //This won't work - I need something else to prevent duplicates.
            //ViewBag.Product.ProductName = new SelectList(db.Products, "Id", "ProductName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrandId,CategoryId,ProductName,Quantity,ExpirationDate,MyProperty")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Info", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
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

        // POST: Products/Edit/5
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

        // GET: Products/Delete
        public ActionResult Delete(int id)
        {
            using (Context context = GetContext())
            {

                //if (id == null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //}
                var product = GetProducts(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
        }

        // POST: Products/Delete
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
