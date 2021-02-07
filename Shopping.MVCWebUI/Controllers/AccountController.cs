using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Shopping.MVCWebUI.Entity;
using Shopping.MVCWebUI.Identity;
using Shopping.MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.MVCWebUI.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();
        private IdentityDataContext dbIdentity = new IdentityDataContext();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        [Authorize]
        public ActionResult Index()
        {
            var orders = db.Orders.AsNoTracking().Where(order => order.UserName == User.Identity.Name)
                .Select(order => new OrderUserModel()
                {
                    OrderId = order.Id,
                    OrderNumber = order.OrderNumber,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice,
                    OrderState = order.OrderState
                }).OrderByDescending(order => order.OrderDate);

            return View(orders.ToList());
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var order = db.Orders.AsNoTracking().Where(o => o.Id == id).Select(o =>
            new OrderDetailsModel()
            {
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
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register newUser)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Name = newUser.Name,
                    Surname = newUser.Surname,
                    UserName = newUser.UserName,
                    Email = newUser.Email
                };
                var result = userManager.Create(user, newUser.Password);

                if (result.Succeeded)
                {
                    if (roleManager.RoleExists("User"))
                    {
                        userManager.AddToRole(user.Id, "User");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("UserRegisterError", "Kullanıcı Oluşturma Hatası");
                }
            }

            return View(newUser);
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = userManager.Find(user.UserName, user.Password);
                if (result != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identity = userManager.CreateIdentity(result, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = user.RememberMe
                    };
                    authManager.SignIn(authProperties, identity);
                    
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    var usr = dbIdentity.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                    Session["userFullName"] = usr.Name + " " + usr.Surname;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Böyle bir kullanıcı yok");
                }
            }

            return View();
        }
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Login");
        }

    }
}