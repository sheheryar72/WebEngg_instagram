using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Models;


namespace WebEngg_instagram.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup()
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
                Models.LoginModel lm = new Models.LoginModel();
                if (lm.logincheck(username, password))
                {
                    Session["user"] = username;
                    TempData["Error"] = "";
                    return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = "Invalid ID/Pass";
            }
            
            return RedirectToAction("Index");
        }
    }
}