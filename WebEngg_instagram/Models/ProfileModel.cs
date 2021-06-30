using System;
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
            string post = "Select * from Post where username = @user order by Post_ID ASC";
            
            string followers = "Select * from followers where username = @user";
            string following = "Select * from followers where Follower_ID = @user";
            
            string likes = "Select * from Likes where Post_ID in (Select Post_ID from Post where username = @user)";
            string comments = "Select * from Comments where Post_ID in (Select Post_ID from Post where username = @user)";

            string No_of_comments = "select Post_ID ,Count(Post_ID) As No_of_Comments from Comments where Post_ID in (Select Post_ID from Post where Username = @user) group by Post_ID order by Post_ID ASC";
            string No_of_likes = "select Post_ID ,Count(Post_ID) As No_of_Likes from Likes where Post_ID in (Select Post_ID from Post where Username = @user) group by Post_ID order by Post_ID ASC";

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
    }
}