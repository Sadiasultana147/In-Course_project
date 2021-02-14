using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MVC_Evidence_Work_01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Evidence_Work_01.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel m)
        {   
            if (ModelState.IsValid)
            {
                var store = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(store);
                var user = manager.Find(m.Username, m.Password);
                if (user != null)
                {
                    var am = HttpContext.GetOwinContext().Authentication;
                    var claim = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    am.SignIn(new AuthenticationProperties { IsPersistent = false }, claim);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed");
                }
            }
          return View(m);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel m)
        {
            if (ModelState.IsValid)
            {
                var store = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(store);
                var user = new IdentityUser { UserName = m.Username };
                var result = manager.Create(user, m.Password);
                if (result.Succeeded)
                {
                    
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Regitration Failed");
                }
            }
            return View(m);
        }
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}