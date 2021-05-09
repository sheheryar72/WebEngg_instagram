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
            DataTable[] dta = new DataTable[4];
            string Userinfo = "Select * from User_Info where username = @user";
            string post = "Select * from Post where username = @user";
            string followers = "Select * from followers where username = @user";
            string following = "Select * from followers where Follower_ID = @user";

            
            dta[0] = DataExtractor(Userinfo, username);
            dta[1] = DataExtractor(post, username);
            dta[2] = DataExtractor(followers, username);
            dta[3] =  DataExtractor(following, username);

            return dta;
        }

        private DataTable DataExtractor(string query,string username)
        {
            DataTable dt = new DataTable();
            SqlDataHelper sdh = new SqlDataHelper();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@user", username);

            dt = sdh.Select(query, param);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else { return dt; }
        }

        public bool SigningUp()
        {
            return true;
        }
    }
}