using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEngg_instagram.Models;

namespace WebEngg_instagram.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ModifyAccount");
        }

        public ActionResult ModifyAccount()
        {
            // TODO : taook all user values from DB to Page
            if (IsUserLoggedIn())
            {
                DataTable dt = new DataTable();
                AccountModel am = new AccountModel();
                string un = Session["user"].ToString();
                dt = am.DataBringer(un);
                PrintDataToTextBox(dt);
                return View();
            }
            else { return RedirectToAction("Login", "User"); }
        }
        [HttpPost]
        public ActionResult ModifyProfile(FormCollection form)
        {
            //TODO: Query is not running properly |||||||Date issue
            string name = form.Get("name").ToString();
            string bio = form.Get("bio").ToString();
            string phone = form.Get("phone").ToString();
            string email = form.Get("email").ToString();
            string city = form.Get("city").ToString();
            string gender = form.Get("gender").ToString();
            string country = form.Get("country").ToString();
            string dob = form.Get("dob").ToString();

            AccountModel am = new AccountModel();
            if(am.ProfileValueUpdate(name, bio, phone, email, city, gender, country, dob ,Session["user"].ToString())==0)
            {
                ViewBag.Message = "Something went wrong";
            }
            return RedirectToAction("ModifyAccount");
        }
        [HttpPost]
        public ActionResult ModifyCredentials(FormCollection form)
        {
            
            string user = form.Get("username");
            string oldpassword = form.Get("oldpassword");
            string newpassword = form.Get("newpassword");
            if (oldpassword != "" && newpassword != null)
            {
                ViewBag.Message = "Please Enter the password again";
                AccountModel am = new AccountModel();
                if (am.CredentialValueUpdate(oldpassword, newpassword, user) == 0)
                {
                    ViewBag.Message = "Incorrect Old password";
                }
            }
            return RedirectToAction("ModifyAccount");
        }

        public void PrintDataToTextBox(DataTable dt)
        {
            ViewData["name"] = dt.Rows[0]["Name"].ToString(); 
            ViewData["bio"] = dt.Rows[0]["Bio"].ToString(); 
            ViewData["phone"] = dt.Rows[0]["Phone"].ToString(); 
            ViewData["email"] = dt.Rows[0]["Email"].ToString(); 
            ViewData["city"] = dt.Rows[0]["City"].ToString(); 
            ViewData["country"] = dt.Rows[0]["Country"].ToString(); 
            ViewData["gender"] = dt.Rows[0]["Gender"].ToString(); 
            ViewData["dob"] = dt.Rows[0]["DOB"].ToString();
            ViewData["username"] = dt.Rows[0]["Username"].ToString();
            ViewData["prof"] = dt.Rows[0]["Profile_Pic"].ToString();
        }
        private bool IsUserLoggedIn()
        {
            return Session["user"] != null;
        }
        [HttpPost]
        public ActionResult ProfilePictureOpenFileDialog(HttpPostedFileBase Picture)
        {
            //string ProfileAddress = Form.Get("Picture");
            try
            {
                if (Picture.ContentLength > 0)
                {
                    string _FileName = System.IO.Path.GetFileName(Picture.FileName);
                    string[] splits = new string[3];
                    splits = _FileName.Split('.');
                    string Folder = "~/Data/Instagram/" + Session["user"];
                    string ProfilePath = Folder + "/" + Session["user"].ToString() + "." + splits[1];
                    string _path = System.IO.Path.Combine(Server.MapPath(ProfilePath));
                    bool exists = System.IO.Directory.Exists(Server.MapPath(Folder));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(Folder));
                    Picture.SaveAs(_path);
                    AccountModel AM = new AccountModel();
                    if(AM.ChangeProfile(ProfilePath.Replace("~",""), Session["user"].ToString()))
                    { ViewBag.Message = "File Uploaded Successfully!!"; }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "File Upload failed!!";
                return View();
            }
            return RedirectToAction("Index");
        }

        private void Profile_Data(string prof)
        {
            //DataTable dt = new DataTable();
            //ProfileModel pm = new ProfileModel();
            //dt = pm.ProfileDataExtractor(prof);
            //if (dt != null || dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[0]["Username"].ToString() == prof)
            //    {
            //        username = dt.Rows[0]["Username"].ToString();
            //        name = dt.Rows[0]["Name"].ToString();
            //        Phone = dt.Rows[0]["Phone"].ToString();
            //        Email = dt.Rows[0]["Email"].ToString();
            //        Country = dt.Rows[0]["Country"].ToString();
            //        Gender = dt.Rows[0]["Gender"].ToString();
            //        DOB = dt.Rows[0]["DOB"].ToString();
            //        bio = dt.Rows[0]["Bio"].ToString();
            //    }
            //}
        }


    }
}