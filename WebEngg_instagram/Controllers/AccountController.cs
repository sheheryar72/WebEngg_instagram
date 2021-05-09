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
        private string username;
        private string name;
        private string Phone;
        private string Email;
        private string Country;
        private string Gender;
        private string DOB;
        private string bio;
        // GET: Account
        public ActionResult Index()
        {
            return View();
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