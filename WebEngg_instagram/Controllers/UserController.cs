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
            if(IsUserLoggedIn())
            {
                return RedirectToAction("MyProfile", "Profile");
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection form)
        {
            string name = form.Get("name");
            string email = form.Get("email");
            string username = form.Get("usernames");
            string password = form.Get("passwords");

            if(name!="" && email!="" && username!="" && password!="")
            {
                LoginModel LM = new LoginModel();
                if(!LM.CheckUser(username))
                {
                    if(LM.SigningUp(name, email, username, password))
                    {
                        return RedirectToAction("Login");
                    }
                }
            }
            return RedirectToAction("Login");
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
        private bool IsUserLoggedIn()
        {
            return Session["user"] != null;
        }

    }
}