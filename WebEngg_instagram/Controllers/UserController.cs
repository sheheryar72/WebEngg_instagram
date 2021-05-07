using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Models;


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
        public ActionResult Authenticate(FormCollection fc)
        {
            string username = fc["Username"];
            string password = fc["Password"];
            if(username != null && password != null)
            {
                Models.LoginModel log = new Models.LoginModel();
                if (log.LoggingIn(username, password))
                {
                    Session["user"] = username;
                    TempData["Error"] = "";
                    return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = "Invalid ID/Pass";
            }
            
            return RedirectToAction("Login");
        }
    }
}