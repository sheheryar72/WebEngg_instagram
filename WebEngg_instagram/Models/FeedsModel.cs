using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebEngg_instagram.Models
{
    public class FeedsModel
    {
        private string connStr;
        public FeedsModel()
        {
            connStr = "Data Source=DESKTOP-0RBAM18\\FA;Initial Catalog=Instagram;Persist Security Info=True;User ID=sa;Password=123";
        }
        public int InsertPost(string PostID, string FileAddress , string FileType, string Text , string Username)
        {
            string dateTime = DateTime.Now.ToString();
            string[] datetimesplit = new string[3];
            datetimesplit = dateTime.Split(' ');
            string date = datetimesplit[0];
            string time = DateTime.Parse(datetimesplit[1] + datetimesplit[2]).ToString("HH:mm:ss");
            string query = "Insert into Post values (@postid , @user , CONVERT (DATE, @date) , @time , @text , @source , @type)";
            string Type = "TEXT";
            DataTable dt = new DataTable();
            //return 1;
            var parameters = new Dictionary<string, object>()
                {
                    { "postid", PostID },
                    { "user", Username},
                    { "date", date},
                    { "time", time},
                    { "text", Text},
                    { "source", FileAddress},
                    { "type", FileType}
                  //  { "ExampleOfNullParam", (object)DBNull.Value }
                };

            DatabaseHelper DH = new DatabaseHelper(connStr);

            DH.Insert(query, Type, parameters).ToString();
            return 1;
        }

        public bool PostIDCheck(string PostID)
        {
            string query = "select Post_ID from Post where Post_ID = @postid";
            DatabaseHelper sdh = new DatabaseHelper(connStr);
            DataTable dt = new DataTable();
            var parameters = new Dictionary<string, object>()
            {
                { "postid", PostID }
            };
            dt = sdh.GetData(query, "Text", parameters);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else { return true; }
        }

        public DataTable GetUsernameOrNameAndProfile(string NameOrUsername)
        {
            DataTable dt = new DataTable();
            // or Name like '%name%'
            string query = "Select * from User_Info where Username like '%@user%'";
            DatabaseHelper DH = new DatabaseHelper(connStr);
            var parameters = new Dictionary<string, object>()
            {
                {"user",NameOrUsername },
                {"name",NameOrUsername }
            };
            dt = DH.GetData(query, "TEXT",parameters);
            return dt;
        }

        public DataTable PostsOfFollowers(string Username)
        {
         //  select p.Post_ID, p.Username,p.post_Date, p.post_Time, p.Text, p.Source, p.Source_Type, u.Profile_Pic,(select count(*) from[likes] where Post_ID = p.Post_ID) as likes_count,
	     //  (select count(*) from[comments] where Post_ID = p.Post_ID) as comments_count from[post] p join[User_Info] u on p.Username = u.Username
         //  where p.Username in (select Username from Followers where Follower_ID = @user) 
            DataTable dt = new DataTable();
            string PS = "PostData";
            string Type = "STOREDPROCEDURE";
            DatabaseHelper DH = new DatabaseHelper(connStr);
            var parameters = new Dictionary<string, object>()
            {
                {"user",Username}
            };
            dt = DH.GetData(PS, Type, parameters);
            return dt;
        }

        public DataTable GetComments(string Username)
        {
            //Select Post_ID,Username,Comment from Comments where Post_ID in ( select Post_ID from post where Username in (select Username from Followers where Follower_ID=@user))
            DataTable dt = new DataTable();
            string PS = "CommentData";
            string Type = "STOREDPROCEDURE";
            DatabaseHelper DH = new DatabaseHelper(connStr);
            var parameters = new Dictionary<string, object>()
            {
                {"user",Username}
            };
            dt = DH.GetData(PS, Type, parameters);
            return dt;
        }

        public string GetProfile(string Username)
        {
            string link = "";
            string query = "Select Profile_pic from User_Info where Username = @user";
            DataTable dt = new DataTable();
            string Type = "TEXT";
            DatabaseHelper DH = new DatabaseHelper(connStr);
            var parameters = new Dictionary<string, object>()
            {
                {"user",Username}
            };
            dt = DH.GetData(query, Type, parameters);
            if(dt.Rows.Count>0)
                link = dt.Rows[0]["Profile_Pic"].ToString();
            
            return link;
        }

    }
}