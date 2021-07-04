using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebEngg_instagram.Models;


namespace WebEngg_instagram.Controllers
{
    public class HomeController : Controller
    {
        private readonly Random _random = new Random();
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public ActionResult Index()
        {
            if (IsUserLoggedIn())
            {
                return RedirectToAction("Home");
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult Home()
        {
            if (/*IsUserLoggedIn()*/true)
            {
                return View();
            }
            return RedirectToAction("Login", "User");
        }
        
        [HttpPost]
        public JsonResult AutoComplete(string ename)
        {
            ArrayList UserNameorName = new ArrayList();
            DataTable dt = new DataTable();
            FeedsModel FM = new FeedsModel();
            dt = FM.GetUsernameOrNameAndProfile(ename);
            if(dt.Rows.Count>0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    UserNameorName.Add(dt.Rows[i]["Username"]);
                }
            }
            return Json(UserNameorName);
        }

        

        [HttpPost]
        public ActionResult AddPost(HttpPostedFileBase PostedFile , string Text)
        {
            string[] AddressAndType = new string[2];
            string Address = "";
            string Format = "";
            string Type = "";

            bool Toggle = true;
            FeedsModel FM = new FeedsModel();
            int SizeOfPostID = 10;
            string PostID = "";
            while (Toggle)
            {
                PostID = RandomString(SizeOfPostID);
                if (FM.PostIDCheck(PostID))
                    Toggle = false;
            }
            AddressAndType = SaveFileAndReturnAddress(PostedFile, PostID);
            Address = AddressAndType[0];
            Format = AddressAndType[1];
            if (Format == "jpeg" || Format == "png" || Format == "jpg" || Format == "bmp" || Format == "tiff" || Format == "gif")
                Type = "Image";
            if (Format == "mp4" || Format == "mpeg" || Format == "mkv" || Format == "avi" || Format == "mkv" || Format == "mkv")
                Type = "Video";
            FM.InsertPost(PostID,Address,Type,Text,Session["user"].ToString());

            return RedirectToAction("MyProfile","Profile");
        }

        public string[] SaveFileAndReturnAddress(HttpPostedFileBase PF , string POSTID)
        {
            string[] returnedString = new string[2];
            try
            {
                if (PF.ContentLength > 0)
                {
                    string _FileName = System.IO.Path.GetFileName(PF.FileName);
                    string[] splits = new string[3];
                    splits = _FileName.Split('.');

                    string Folder = "~/Data/Instagram/" + Session["user"] + "/post/" + POSTID;
                    
                    string ProfilePath = Folder + "/" + POSTID + "." + splits[1];
                    string _path = System.IO.Path.Combine(Server.MapPath(ProfilePath));
                    bool exists = System.IO.Directory.Exists(Server.MapPath(Folder));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(Folder));
                    PF.SaveAs(_path);
                    returnedString[0] = ProfilePath.Replace("~","");
                    returnedString[1] = splits[1];

                    return returnedString;
                }
            }
            catch
            {
                ViewBag.Message = "Post Upload failed!!";
            }
            return returnedString;
        }
        private bool IsUserLoggedIn()
        {
            return Session["user"] != null;
        }


    }
}