using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Identity
{
    public class IdentityDataInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var roleStore = new RoleStore<ApplicationRole>(context);
                RoleManager<ApplicationRole> roleManager;
                roleManager = new RoleManager<ApplicationRole>(roleStore);
                var role = new ApplicationRole(){Name = "Admin", Description = "Yönetici"};
                roleManager.Create(role);
            }
            if (!context.Roles.Any(role => role.Name == "User"))
            {
                var roleStore = new RoleStore<ApplicationRole>(context);
                RoleManager<ApplicationRole> roleManager;
                roleManager = new RoleManager<ApplicationRole>(roleStore);
                var role = new ApplicationRole(){ Name = "User", Description = "Standart Kullanıcı" };
                roleManager.Create(role);
            }
            if (!context.Users.Any(user => user.UserName == "huso"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser(){Name = "Hüseyin", Surname = "Altunel", UserName = "huso", Email = "abc@hotmail.com"};
                var result = userManager.Create(user,"123456");
                userManager.AddToRole(user.Id,"Admin");
                userManager.AddToRole(user.Id, "User");
            }


            base.Seed(context);
        }
    }
}