﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shopping.MVCWebUI.Entity;

namespace Shopping.MVCWebUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SubCategoryController : Controller
    {
        private DataContext db = new DataContext();

        // GET: SubCategory
        public ActionResult Index()
        {
            var subCategories = db.SubCategories.AsNoTracking().Where(s => s.IsActive == EnumIsActiveState.Active).Include(s => s.Category);
            return View(subCategories.ToList());
        }

        // GET: SubCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // GET: SubCategory/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.AsNoTracking().Where(s => s.IsActive == EnumIsActiveState.Active), "Id", "Name");
            return View(new SubCategory());
        }

        // POST: SubCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,IsActive,CategoryId")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.SubCategories.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories.AsNoTracking(), "Id", "Name", subCategory.CategoryId);
            return View(subCategory);
        }

        // GET: SubCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories.AsNoTracking().Where(s => s.IsActive == EnumIsActiveState.Active), "Id", "Name", subCategory.CategoryId);
            return View(subCategory);
        }

        // POST: SubCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,IsActive,CategoryId")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.AsNoTracking(), "Id", "Name", subCategory.CategoryId);
            return View(subCategory);
        }

        // GET: SubCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            subCategory.IsActive = EnumIsActiveState.Deleted;
            db.Entry(subCategory).State = EntityState.Modified;
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
