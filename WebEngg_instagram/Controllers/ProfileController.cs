using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEngg_instagram.Models;

namespace WebEngg_instagram.Controllers
{
    public class ProfileController : Controller
    {
        private string name = "";
        private string bio = "";
        private string prof_addr = "";

        ArrayList followers_list = new ArrayList();
        ArrayList following_list = new ArrayList();

        ArrayList likes_list = new ArrayList();
        ArrayList comments_list = new ArrayList();
        ArrayList comments_list_un = new ArrayList();
        
        ArrayList posts_list = new ArrayList();
        ArrayList posts_ID_list = new ArrayList();

        ArrayList No_of_likes = new ArrayList();
        ArrayList No_of_comments = new ArrayList();

        public ActionResult Index()
        {

            return RedirectToAction("YourProfile");
        }

        public ActionResult MyProfile()
        {
            if (IsUserLoggedIn())
            {
                TempData["userprofile"] = Session["user"];
                Profile_Data(Session["user"].ToString());
                return View();
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public ActionResult OtherUsers(string username)
        {
            TempData["userprofile"] = username;
            Profile_Data(username);
            return View();
        }

        public ActionResult Signout()
        {
            if(IsUserLoggedIn())
            {
                Session["User"] = null;
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("Login", "User");
        }
        private void Profile_Data(string prof)
        {
            DataTable[] dt = new DataTable[8];    

            ProfileModel pm = new ProfileModel();
            dt = pm.ProfileDataExtractor(prof);     //1: user , 2: post , 3:followers , 4: followings ,5 : likes , 6: comments ,7: No of likes , 8: No of comments per post

            ProfileData(dt[0]); // user

            posts_list = Data_Extractor(dt[1], "Source"); // post
            posts_list = Data_Extractor(dt[1], "Source"); // post

            followers_list = Data_Extractor(dt[2], "Follower_ID");  // followers
            following_list = Data_Extractor(dt[3], "Username"); // followings

            likes_list = Data_Extractor(dt[4], "Username"); // Likes

            comments_list_un = Data_Extractor(dt[5], "Username"); // Comments
            comments_list = Data_Extractor(dt[5], "comment"); // Comments

            No_of_likes = Data_Extractor(dt[6], "No_of_Likes"); // Comments
            No_of_comments = Data_Extractor(dt[7], "No_of_Comments"); // Comments

            HTML_Data_Print(name, prof_addr, bio);
        }

        public void ProfileData(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0) // 1: User
            {
                prof_addr = dt.Rows[0]["Profile_Pic"].ToString();
                name = dt.Rows[0]["Name"].ToString();
                bio = dt.Rows[0]["Bio"].ToString();
               
            }
            else { prof_addr = ""; name = ""; bio = ""; }
        }

        public ArrayList Data_Extractor(DataTable dt , string val)
        {
            ArrayList temp = new ArrayList();
            if (dt != null && dt.Rows.Count > 0) // 1: Post
            {
                foreach (DataRow dr in dt.Rows)
                {
                    temp.Add(dr[val].ToString());
                }
                return temp;
            }
            else { temp = null; return temp; }
        }

        public void HTML_Data_Print(string name , string prof_address , string bio)
        {
            TempData["name"] = name;
            TempData["bio"] = bio;
            TempData["picture"] = prof_addr;
            try
            {
                if(followers_list.Count<0)
                {
                    //TODO: Sort out error of open page for new user.....and also for user who has not post anything
                }
                
                TempData["follower"] = followers_list.Count;
                TempData["following"] = following_list.Count;
                TempData["post"] = posts_list.Count;



                TempData["Likes"] = likes_list.Count;
                TempData["Comments"] = comments_list.Count;
            }
            catch(Exception ex)
            { }

            for (int i = 0; i < posts_list.Count; i++)
            {
                ViewData["post"+i.ToString()] = posts_list[i]; // show posts
                if (i < No_of_likes.Count)
                { ViewData["likes" + i.ToString()] = No_of_likes[i]; }
                if(i< No_of_comments.Count)
                { ViewData["comments" + i.ToString()] = No_of_comments[i]; }
            }
        }


        private bool IsUserLoggedIn()
        {
            return Session["user"] != null;
        }
    }
}