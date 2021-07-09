﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebEngg_instagram.Models
{
    public class ProfileModel
    {
        private string connStr;
        
        public ProfileModel()
        {
            connStr = "Data Source=DESKTOP-0RBAM18\\FA;Initial Catalog=Instagram;Persist Security Info=True;User ID=sa;Password=123";
        }

        public DataTable[] ProfileDataExtractor(string username)
        {
            DataTable[] dta = new DataTable[8];
            
            string Userinfo = "Select * from User_Info where username = @user";

            string post = "Select * from Post where username = @user";
            
            string followers = "Select * from followers where username = @user";
            string following = "Select * from followers where Follower_ID = @user";
            
            string likes = "Select * from Likes where Post_ID in (Select Post_ID from Post where username = @user)";
            string comments = "Select * from Comments where Post_ID in (Select Post_ID from Post where username = @user)";

            string No_of_comments = "select P.Post_ID , Count(C.Post_ID) as No_of_comments from Post P left join Comments C on P.Post_ID = C.Post_ID where P.Username = @user group by P.Post_ID ";
            string No_of_likes = "select P.Post_ID , Count(L.Post_ID) as No_of_likes from Post P left join Likes L on P.Post_ID = L.Post_ID where P.Username = @user group by P.Post_ID";

            dta[0] = DataExtractor(Userinfo, username);
            dta[1] = DataExtractor(post, username);

            dta[2] = DataExtractor(followers, username);
            dta[3] =  DataExtractor(following, username);

            dta[4] = DataExtractor(likes, username);
            dta[5] = DataExtractor(comments, username);

            dta[6] = DataExtractor(No_of_likes, username);
            dta[7] = DataExtractor(No_of_comments,username);

            return dta;
        }

        private DataTable DataExtractor(string query,string username)
        {
            DataTable dt = new DataTable();
            string Type = "TEXT";
            var parameters = new Dictionary<string, object>()
                {
                    { "user", username },
                    { "ExampleOfNullParam", (object)DBNull.Value }
                };

            DatabaseHelper dh = new DatabaseHelper(connStr);
            if (username != "")
            {
                dt = dh.GetData(query, Type , parameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
            }
            dt = null;
            return dt; 
        }

        public bool SigningUp()
        {
            return true;
        }
    
        public bool IsUserFollowed(string Username , string FollowerUsername)
        {
            string query = "Select * from Followers where Username = @followerusername and Follower_ID = @user";
            DataTable dt = new DataTable();
            string Type = "TEXT";
            var parameters = new Dictionary<string, object>()
                {
                    { "user", Username },
                    { "followerusername", FollowerUsername },
                    { "ExampleOfNullParam", (object)DBNull.Value }
                };

            DatabaseHelper dh = new DatabaseHelper(connStr);
            if (Username != "" && FollowerUsername!="")
            {
                dt = dh.GetData(query, Type, parameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Follow(string Username, string FollowerUsername)
        {
            string query = "Insert into Followers values(@follower,@user)";
            string Type = "TEXT";
            DataTable dt = new DataTable();
            var parameters = new Dictionary<string, object>()
                {
                    { "follower", FollowerUsername },
                    { "user", Username}
                  //  { "ExampleOfNullParam", (object)DBNull.Value }
                };

            DatabaseHelper dh = new DatabaseHelper(connStr);

            dh.Insert(query, Type, parameters);

            //return true;
        }

        public void UnFollow(string Username, string FollowerUsername)
        {
            string query = "Delete from Followers where Username = @follower and Follower_ID = @user";
            string Type = "TEXT";
            DataTable dt = new DataTable();
            var parameters = new Dictionary<string, object>()
                {
                    { "follower", FollowerUsername},
                    { "user", Username }

                  //  { "ExampleOfNullParam", (object)DBNull.Value }
                };
            DatabaseHelper dh = new DatabaseHelper(connStr);
            dh.UpdateOrDelete(query, Type, parameters);
            //return true;
        }
    }
}