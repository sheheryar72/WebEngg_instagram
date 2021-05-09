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

        ArrayList posts_list = new ArrayList();


        public ActionResult Index()
        {

            return RedirectToAction("profile");
        }

        public ActionResult profile()
        {
            if (IsUserLoggedIn())
            {
                TempData["userprofile"] = Session["user"];
                Profile_Data(Session["user"].ToString());
                return View();
            }
            return RedirectToAction("Login", "User");
        }

        private void Profile_Data(string prof)
        {
            DataTable[] dt = new DataTable[4];    
            ProfileModel pm = new ProfileModel();
            dt = pm.ProfileDataExtractor(prof);     //1: user , 2: post , 3:followers , 4: followings


            if (dt[0] != null && dt[0].Rows.Count > 0) // 1: User
            {
                prof_addr = dt[0].Rows[0]["Profile_Pic"].ToString();
                name = dt[0].Rows[0]["Name"].ToString();
                bio = dt[0].Rows[0]["Bio"].ToString();
                Set_Profile(name, prof_addr ,bio);
            }else { }


            //TODO : work pending here
            if (dt[1] != null && dt[1].Rows.Count > 0) // 1: Post
            {

                foreach (DataRow dr in dt[1].Rows)
                {
                    
                    posts_list.Add(dr["Source"].ToString());
                }
            }
            else{ }
            
            if (dt[2] != null && dt[2].Rows.Count > 0) // 1: Followers
            {
                foreach(DataRow dr in dt[2].Rows)
                {
                    followers_list.Add(dr["Follower_ID"].ToString());
                }
            }
            else
            { }
            
            if (dt[3] != null && dt[3].Rows.Count > 0) // 1: Following
            {
                foreach (DataRow dr in dt[3].Rows)
                {
                    following_list.Add(dr["Username"].ToString());
                }
            }
            else{  }
            Set_Profile();
        }

        public void Set_Profile(string name , string prof_address , string bio)
        {
            TempData["name"] = name;
            TempData["bio"] = bio;
            TempData["picture"] = prof_addr;
            

            // TODO print value on page
        }

        public void Set_Profile()
        {
            TempData["follower"] = followers_list.Count;
            TempData["following"] = following_list.Count;
            TempData["post"] = posts_list.Count;
            for(int i = 0; i < posts_list.Count; i++)
            {
                ViewData[i.ToString()] = posts_list[i];
            }

            // TODO print value on page
        }

        private bool IsUserLoggedIn()
        {
            return Session["user"] != null;
        }
    }
}