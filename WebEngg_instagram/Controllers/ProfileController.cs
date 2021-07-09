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

            return RedirectToAction("MyProfile");
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
        [HttpGet]
        public ActionResult OthersProfile(string username)
        {
            TempData["userprofile"] = username;
            Session["userprofile"] = username;
            if(IsUserLoggedIn())
            {
                if (username == Session["user"].ToString())
                {
                    return RedirectToAction("MyProfile");
                }
                ProfileModel PM = new ProfileModel();
                if (PM.IsUserFollowed(Session["user"].ToString(), username))
                    Session["IsFollowed"] = "Following";
                else
                    Session["IsFollowed"] = "Follow";
                Profile_Data(username);
                return View();
            }
            else
                return RedirectToAction("Login", "User");
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
            FollowersAndFollowingsData();
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
        public void FUF()
        {
            ProfileModel PM = new ProfileModel();
            //string Follower = User;
            string Follower = Session["userprofile"].ToString();
            string username = Session["user"].ToString();

            if (Session["IsFollowed"].ToString() == "Following") //Delete
            {
                PM.UnFollow(username, Follower);
            }
            if (Session["IsFollowed"].ToString() == "Follow") //Insert
            {
                PM.Follow(username, Follower);
            }
        }
        public ActionResult FollowUnFollow()
        {
            string user = Session["userprofile"].ToString();
            FUF();


            return RedirectToAction("OthersProfile", "Profile", new {username = user});


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
                if (followers_list!=null)
                {
                    TempData["follower"] = followers_list.Count;
                }
                else { TempData["follower"] = 0; }
                if (posts_list != null)
                {
                    TempData["post"] = posts_list.Count;
                }
                else { TempData["post"] = 0; }

                if (following_list != null)
                {
                    TempData["following"] = following_list.Count;
                }
                else { TempData["following"] = 0; }

                if (likes_list != null)
                {
                    TempData["Likes"] = likes_list.Count;
                }
                else { TempData["Likes"] = 0; }

                if (comments_list != null)
                {
                    TempData["Comments"] = comments_list.Count;
                }
                else { TempData["Comments"] = 0; }

            }
            catch (Exception ex)
            { }
            if (posts_list != null)
            {
                for (int i = 0; i < posts_list.Count; i++)
                {
                    ViewData["post" + i.ToString()] = posts_list[i]; // show posts
                    if (No_of_likes != null)
                        ViewData["likes" + i.ToString()] = No_of_likes[i];
                    if (No_of_comments != null)
                       ViewData["comments" + i.ToString()] = No_of_comments[i];
                }
            }
        }
        private bool IsUserLoggedIn()
        {
            return Session["user"] != null;
        }

        public void FollowersAndFollowingsData()
        {
            int i = 0 , j = 0;
            if(followers_list != null)
            {
                foreach (string follower in followers_list)
                {
                    ViewData["Follower" + i.ToString()] = follower;
                    i++;
                }
            }
            ViewData["FollowerCount"] = i;

            if (following_list != null)
            {
                foreach (string following in following_list)
                {
                    ViewData["Following" + j.ToString()] = following;
                    j++;
                }
            }
            ViewData["FollowingCount"] = j;
        }
    }
}