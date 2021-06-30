using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEngg_instagram.Models;

namespace WebEngg_instagram.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPersonalUserData(FormCollection form)
        {
            string ID = form.Get("User");
            DataTable dt = new DataTable();
            AccountModel am = new AccountModel();
            dt = am.DataBringer(ID);


            var parameters = new Dictionary<string, object>();
            foreach(DataColumn dc in dt.Columns)
            {
                string name = dc.ColumnName;
                string val = dt.Rows[0][name].ToString();

                parameters.Add(name, val);
            }

            return Json(parameters, JsonRequestBehavior.AllowGet);
        }
    }
}