using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEngg_instagram.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (IsUserLoggedIn())
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        private bool IsUserLoggedIn()
        {
            return Session["user"] != null;
        }

        public ActionResult check()
        {
            return View();
        }
    }
}