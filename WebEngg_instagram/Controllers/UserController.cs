using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEngg_instagram.Models;


namespace WebEngg_instagram.Controllers
{
    public class UserController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult RecoverYourAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(FormCollection form)
        {
            string username = form["Username"];
            string password = form["Password"];

            if(username != null && password != null)
            {
                LoginModel log = new LoginModel();
                if (log.LoggingIn(username, password))
                {
                    Session["user"] = username;
                    TempData["Error"] = "";
                    return RedirectToAction("MyProfile", "Profile");
                }
                TempData["Error"] = "Invalid ID/Pass";
            }
            
            return RedirectToAction("Login");
        }
    }
}