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

    }
}