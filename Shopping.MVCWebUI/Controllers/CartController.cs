using Shopping.MVCWebUI.Entity;
using Shopping.MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.MVCWebUI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {

            return View(GetCart());
        }
        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.AsNoTracking().FirstOrDefault(prd => prd.Id == Id);
            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

            return RedirectToAction("Index","Home");
        }
        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.AsNoTracking().FirstOrDefault(prd => prd.Id == Id);
            GetCart().DeleteProduct(product);

            return RedirectToAction("Index");
        }
        public PartialViewResult CartSummary()
        {
            return PartialView(GetCart());
        }
        [Authorize]
        public ActionResult CheckOut()
        {

            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult CheckOut(ShippingDetails details)
        {
            if (ModelState.IsValid)
            {
                var cart = GetCart();
                if (cart.CartLines.Count() != 0)
                {
                    var mail = new Mail();
                    var userFullName = Session["userFullName"].ToString();


                    MailDetails mailDetails = new MailDetails() {FullName = userFullName, UserName = details.UserName, AddressTitle = details.AddressTitle, Address = details.Address,  City = details.City, District = details.District, Neighborhood = details.Neighborhood, ZipCode = details.ZipCode }; 
                    mail.sendMail(cart,mailDetails);
                    ViewBag.mailException = mail.exception;
                    if (String.IsNullOrEmpty(ViewBag.mailException))
                    {
                        ViewBag.message = "success";
                        SaveOrder(cart, details);
                        DecreaseStocks(cart.CartLines);                        
                    }
                    else
                    {
                        ViewBag.message = "error";
                        return View(details);
                    }

                    cart.Clear();
                }
                else
                {
                    ModelState.AddModelError("ProductError", "Sepetinizde ürün bulunamadı");
                }
            }

            return View(details);
        }

        private void SaveOrder(Cart cart, ShippingDetails details)
        {
            var order = new Order();
            order.OrderNumber = "S" + (new Random().Next(100000, 1000000)).ToString();
            order.TotalPrice = cart.TotalPrice();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;

            order.UserName = User.Identity.Name;
            order.AddressTitle = details.AddressTitle;
            order.Address = details.Address;
            order.City = details.City;
            order.District = details.District;
            order.Neighborhood = details.Neighborhood;
            order.ZipCode = details.ZipCode;

            order.OrderLines = new List<OrderLine>();

            foreach (var cLine in cart.CartLines)
            {
                var orderLine = new OrderLine()
                {
                    ProductId = cLine.Product.Id,
                    Quantity = cLine.Quantity,
                    Price = cLine.Product.Price * cLine.Quantity

                };
                order.OrderLines.Add(orderLine);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }
        public void DecreaseStocks(List<CartLine> cartLines)
        {
            cartLines.ForEach(line =>
            {
                line.Product.Stock -= line.Quantity;
                line.Product.QuantitySold += line.Quantity;
                var dbProduct = db.Products.AsNoTracking().Where(prd => prd.Id == line.Product.Id).FirstOrDefault();
                dbProduct = line.Product;
                db.Entry(dbProduct).State = EntityState.Modified;
            });
            db.SaveChanges();
        }

    }
}