using Shopping.MVCWebUI.Entity;
using Shopping.MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.MVCWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            var orders = db.Orders.AsNoTracking().Select(o => new OrderAdminModel()
            {
                UserName = o.UserName,
                OrderId = o.Id,
                OrderNumber = o.OrderNumber,
                TotalPrice = o.TotalPrice,
                OrderState = o.OrderState,
                OrderDate = o.OrderDate,
                Count = o.OrderLines.Count
            }).OrderByDescending(o => o.OrderDate);

            return View(orders.ToList());
        }
        public ActionResult Details(int id)
        {
            var order = db.Orders.AsNoTracking().Where(o => o.Id == id).Select(o =>
            new OrderDetailsModel()
            {
                OrderId = o.Id,
                OrderState = o.OrderState,
                AddressTitle = o.AddressTitle,
                Address = o.Address,
                City = o.City,
                District = o.District,
                Neighborhood = o.Neighborhood,
                OrderLines = o.OrderLines.Select(oLine => new OrderLineModel()
                {
                    ProductId = oLine.ProductId,
                    ProductName = oLine.Product.Name,
                    Image = oLine.Product.DefaultImage,
                    Quantity = oLine.Quantity,
                    Price = oLine.Price
                }).ToList()
            }).FirstOrDefault();

            return View(order);
        }
        public ActionResult EditOrderState(int OrderId, EnumOrderState OrderState)
        {
            if (ModelState.IsValid)
            {
                var order = db.Orders.Find(OrderId);
                if (order != null && order.OrderState != OrderState)
                {
                    order.OrderState = OrderState;
                    db.SaveChanges();
                    TempData["success"] = "Bilgiler başarıyla kayıt edildi";
                }
                else
                {
                    TempData["failed"] = "Bir değişiklik yapmadınız";
                }
            }
            return RedirectToAction("Details", new { id = OrderId });
        }
    }
}