using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using prjProyecto.Models;
using prjProyecto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace prjProyecto.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        [Authorize(Roles="Administrador")]
        public ActionResult Index()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = UserManager.Users.ToList();
            var usersView = new List<UserView>();

            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Name = user.UserName,
                    UserId = user.Id
                };
                usersView.Add(userView);
            }
            return View(usersView);
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult Roles(string userID)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var roles = roleManager.Roles.ToList();
            var users = UserManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            var rolesView = new List<RoleView>();
            if (user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    var role = roles.Find(r => r.Id == item.RoleId);
                    var roleView = new RoleView
                    {
                        RoleId = role.Id,
                        Name = role.Name
                    };
                    rolesView.Add(roleView);
                }
            }
            else
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id,
                Roles = rolesView
            };

            return View(userView);
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult AddRole(string userID)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = UserManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var list = roleManager.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = "[Select a role...]" });
            list = list.OrderBy(r => r.Name).ToList();
            ViewBag.RoleId = new SelectList(list, "Id", "Name");

            return View(userView);
        }
        [HttpPost]
        public ActionResult AddRole(string userID, FormCollection form)
        {
            var roleId = Request["RoleId"];

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = UserManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id
            };

            if (string.IsNullOrEmpty(roleId))
            {
                ViewBag.Error = "You must select a role";

                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Select a role...]" });
                list = list.OrderBy(r => r.Name).ToList();
                ViewBag.RoleId = new SelectList(list, "Id", "Name");

                return View(userView);
            }

            var roles = roleManager.Roles.ToList();
            var role = roles.Find(r => r.Id == roleId);

            if (!UserManager.IsInRole(userID, role.Name))
            {
                UserManager.AddToRole(userID, role.Name);
            }

            var rolesView = new List<RoleView>();
            if (user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    role = roles.Find(r => r.Id == item.RoleId);
                    var roleView = new RoleView
                    {
                        RoleId = role.Id,
                        Name = role.Name
                    };
                    rolesView.Add(roleView);
                }
            }
            else
            {
                return HttpNotFound();
            }

            userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = UserManager.Users.ToList().Find(u => u.Id == userID);
            var role = roleManager.Roles.ToList().Find(u => u.Id == roleID);

            if (UserManager.IsInRole(user.Id, role.Name))
            {
                UserManager.RemoveFromRole(user.Id, role.Name);
            }

            var users = UserManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);

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