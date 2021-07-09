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
        public ActionResult ResetPassword(string Email)
        {
            
                
            //string To = Email, UserID, Password, SMTPPort, Host;
            //string token = WebSecurity.GeneratePasswordResetToken(UserName);
            //if (token == null)
            //{
            //    // If user does not exist or is not confirmed.
            //    return View("Index");
            //}
            //else
            //{

            //    //Create URL with above token
            //    var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = UserName, code = token }, "http") + "'>Reset Password</a>";
            //    //HTML Template for Send email
            //    string subject = "Your changed password";
            //    string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
            //    //Get and set the AppSettings using configuration manager.
            //    EmailManager.AppSettings(out UserID, out Password, out SMTPPort, out Host);
            //    //Call send email methods.
            //    EmailManager.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);
            //}
                
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
                    return RedirectToAction("Feeds", "Home");
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