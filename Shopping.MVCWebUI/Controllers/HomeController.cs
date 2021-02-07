﻿using Shopping.MVCWebUI.Entity;
using Shopping.MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.MVCWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        double productCountPerPage = 15;
        double productCount = 0;

        public ActionResult Index(int? id, int? subId, int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            int skipCount = (int)productCountPerPage * (int)(page - 1);

            var products = db.Products.AsNoTracking().Where(prd => prd.IsHome)
                .Select(prd => new ProductModel()
                {
                    Id = prd.Id,
                    Name = prd.Name,
                    Description = prd.Description,
                    Price = prd.Price,
                    DiscountedPrice = prd.DiscountedPrice,
                    CampaignStatus = prd.CampaignStatus,
                    Stock = prd.Stock,
                    Image = prd.DefaultImage ?? "nullimage.png",
                    SubCategoryId = prd.SubCategoryId,
                    SubCategory = prd.SubCategory,
                    Campaign = new CampaignModel()
                    {
                        DueDate = (DateTime?)(prd.Campaign.DueDate),
                        DiscountPercent = (int?)(prd.Campaign.DiscountPercent)
                    }
                }).AsQueryable();


            ViewBag.productsAtCampaign = products.Where(prd => prd.CampaignStatus == true && prd.DiscountedPrice != 0).ToList();

            if (id != null)
            {
                products = products.Where(prd => prd.SubCategory.CategoryId == id);
                productCount = products.Count();
                if (productCount > productCountPerPage)
                {
                    products = products.OrderBy(p => p.Id).Skip(skipCount).Take((int)productCountPerPage);

                }
            }
            else if (subId != null)
            {
                products = products.Where(prd => prd.SubCategoryId == subId);
                productCount = products.Count();
                if (productCount > productCountPerPage)
                {
                    products = products.OrderBy(p => p.Id).Skip(skipCount).Take((int)productCountPerPage);
                }
            }
            else
            {
                productCount = products.Count();
                products = products.OrderBy(p => p.Id).Skip(skipCount).Take((int)productCountPerPage);
            }

            var pageCount = productCount / productCountPerPage;
            if (pageCount % 2 != 0)
            {
                pageCount = (int)pageCount + 1;
            }
            ViewBag.subId = subId;
            ViewBag.productCount = productCount;
            ViewBag.page = page;
            ViewBag.pageCount = pageCount;

            return View(products.ToList());
        }
        public ActionResult Details(int id)
        {
            var product = db.Products.Find(id);
            product.DefaultImage = product.DefaultImage ?? "nullimage.png";
            return View(product);
        }

        public PartialViewResult Categories()
        {
            var categories = db.Categories.AsNoTracking().Select(c => new CategoryModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                SubCategories = c.SubCategories.ToList()
            });

            return PartialView(categories.ToList());
        }


    }
}