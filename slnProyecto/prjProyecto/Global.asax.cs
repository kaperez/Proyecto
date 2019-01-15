using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using prjProyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace prjProyecto
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.ProyectoContext, Migrations.Configuration>());
            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);
            CreateSuperUser(db);
            AddPermissionsToSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private void AddPermissionsToSuperUser(ApplicationDbContext db)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = UserManager.FindByName("admin@asovensep.ec");
            if (!UserManager.IsInRole(user.Id, "Administrador"))
            {
                UserManager.AddToRole(user.Id, "Administrador");
            }

            if (!UserManager.IsInRole(user.Id, "Asociado"))
            {
                UserManager.AddToRole(user.Id, "Asociado");
            }

            if (!UserManager.IsInRole(user.Id, "Contador"))
            {
                UserManager.AddToRole(user.Id, "Contador");
            }

            if (!UserManager.IsInRole(user.Id, "View"))
            {
                UserManager.AddToRole(user.Id, "View");
            }

            if (!UserManager.IsInRole(user.Id, "Create"))
            {
                UserManager.AddToRole(user.Id, "Create");
            }

            if (!UserManager.IsInRole(user.Id, "Edit"))
            {
                UserManager.AddToRole(user.Id, "Edit");
            }

            if (!UserManager.IsInRole(user.Id, "Delete"))
            {
                UserManager.AddToRole(user.Id, "Delete");
            }
        }

        private void CreateSuperUser(ApplicationDbContext db)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = UserManager.FindByName("admin@asovensep.ec");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin@asovensep.ec",
                    Email = "admin@asovensep.ec"
                };
                UserManager.Create(user, "admin@asovensep.ec");
            }
        }

        private void CreateRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }
            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }
            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }
            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }
            if (!roleManager.RoleExists("Administrador"))
            {
                roleManager.Create(new IdentityRole("Administrador"));
            }
            if (!roleManager.RoleExists("Asociado"))
            {
                roleManager.Create(new IdentityRole("Asociado"));
            }
            if (!roleManager.RoleExists("Contador"))
            {
                roleManager.Create(new IdentityRole("Contador"));
            }
        }
    }
}
