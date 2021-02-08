using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shopping.MVCWebUI.Entity;
using Shopping.MVCWebUI.Models;

namespace Shopping.MVCWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Where(p => p.IsActive == EnumIsActiveState.Active).Include(p => p.SubCategory);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
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
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.AsNoTracking(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,CampaignStatus,CampaignId,Stock,Images,IsHome,IsActive,SubCategoryId")] CreateProductModel product)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CampaignStatus = product.CampaignStatus,
                    CampaignId = product.CampaignId,
                    Stock = product.Stock,
                    IsHome = product.IsHome,
                    IsActive = product.IsActive,
                    SubCategoryId = product.SubCategoryId
                };

                var files = product.Images;
                string images = "";
                if (files[0] != null)
                {
                    int imageIndex = 0;
                    foreach (var file in files)
                    {
                        string pic = Path.GetFileName(file.FileName);
                        string path = Path.Combine(
                        Server.MapPath("~/Upload"), pic);

                        file.SaveAs(path);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }
                        images += (file.FileName + ",");
                        imageIndex++;
                    }
                    string[] arrayImages = images.Split(',');
                    newProduct.DefaultImage = arrayImages[0];
                    newProduct.Images = images;
                }
               
                db.Products.Add(newProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubCategoryId = new SelectList(db.SubCategories.AsNoTracking(), "Id", "Name", product.SubCategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Where(prd => prd.Id == id).Select(prd => new CreateProductModel()
            {
                Name = prd.Name,
                Description = prd.Description,
                IsHome = prd.IsHome,
                Price = prd.Price,
                CampaignStatus = prd.CampaignStatus,
                CampaignId = prd.CampaignId,
                Stock = prd.Stock,
                SubCategoryId = prd.SubCategoryId,

            }).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.SubCategoryId = new SelectList(db.SubCategories.AsNoTracking(), "Id", "Name", product.SubCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,CampaignStatus,CampaignId,Stock,Images,IsHome,IsActive,SubCategoryId")] CreateProductModel product)
        {
            if (ModelState.IsValid)
            {
                var oldProductImages= db.Products.AsNoTracking().Where(prd => prd.Id == product.Id).Select(prd => prd.Images).FirstOrDefault(); 
                oldProductImages = oldProductImages == null ? "" : oldProductImages;
                string[] arrayImages = oldProductImages.Split(',');
                var campaignDiscountPercent = db.Campaigns.AsNoTracking().Where(cp => cp.Id == product.CampaignId ).Select(cp => cp.DiscountPercent).FirstOrDefault();
                double discountedPrice = product.Price - product.Price * campaignDiscountPercent / 100;
                if (campaignDiscountPercent == 0)
                {
                    product.CampaignId = null;
                    discountedPrice = 0;
                }
                
                var editedProduct = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    DiscountedPrice = discountedPrice,
                    CampaignStatus = product.CampaignStatus,
                    CampaignId = product.CampaignId,
                    Stock = product.Stock,
                    QuantitySold = product.QuantitySold,
                    IsHome = product.IsHome,
                    IsActive = product.IsActive,
                    Images = oldProductImages,
                    DefaultImage = arrayImages[0],
                    SubCategoryId = product.SubCategoryId
                };

                var files = product.Images;
                string images = "";
                if (files[0] != null)
                {
                    int imageIndex = 0;
                    foreach (var file in files)
                    {
                        string pic = Path.GetFileName(file.FileName);
                        string path = Path.Combine(
                        Server.MapPath("~/Upload"), pic);

                        file.SaveAs(path);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }
                        images += (file.FileName + ",");
                        imageIndex++;
                    }

                    arrayImages = images.Split(',');
                    editedProduct.DefaultImage = arrayImages[0];
                    editedProduct.Images = images;
                }
                db.Entry(editedProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.AsNoTracking(), "Id", "Name", product.SubCategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            product.IsActive = EnumIsActiveState.Deleted;
            db.Entry(product).State = EntityState.Modified;
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
