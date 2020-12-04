using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SirCoPOS.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!(model.UserName == "admin"
                //&& model.Password == "123"
                && model.Password.StartsWith("123")
                ))
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }

            var auth = HttpContext.GetOwinContext().Authentication;
            auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));
            int uid;
            if(model.Password.StartsWith("123") && int.TryParse(model.Password.Substring(3), out uid))
                identity.AddClaim(new Claim(Helpers.Claims.SirCoUID, $"{uid}"));
            else
                identity.AddClaim(new Claim(Helpers.Claims.SirCoUID, $"{99}"));

            auth.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            }, identity);

            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            var auth = HttpContext.GetOwinContext().Authentication;
            auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}