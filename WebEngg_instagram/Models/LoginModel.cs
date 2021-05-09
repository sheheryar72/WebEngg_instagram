using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace WebEngg_instagram.Models
{
    public class LoginModel
    {
        private string connStr;

        public LoginModel()
        {
           connStr = "Data Source=DESKTOP-0RBAM18\\FA;Initial Catalog=Instagram;Persist Security Info=True;User ID=sa;Password=123";
        }

        public bool LoggingIn(string username ,string password)
        {
            string query = "Select * from Credential where username = @user and password = @pass";
            DataTable dt = new DataTable();
            SqlDataHelper sdh = new SqlDataHelper();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@user", username);
            param[1] = new SqlParameter("@pass", password);

            dt = sdh.Select(query, param);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Username"].ToString() == username && dt.Rows[0]["Password"].ToString() == password)
                {
                    return true;
                }
                else
                {  return false; }
            }
            else { return false; }
        }
        public bool SigningUp()
        {
            return true;
        }
    }
}

//SqlConnection connection = new SqlConnection(connStr);
//SqlCommand cmd = connection.CreateCommand();

//string query = "Select * from Credential where username = @user";
//cmd.CommandText = query;

//cmd.Parameters.AddWithValue("@user", username);
//connection.Open();

//SqlDataReader reader = cmd.ExecuteReader();
//DataTable dt = new DataTable();

//dt.Load(reader);

//if (dt != null && dt.Rows.Count > 0)
//{
//    if (dt.Rows[0]["Username"].ToString() == username && dt.Rows[0]["Password"].ToString() == password)
//    {
//        return true;
//    }
//    else
//    { return false; }
//}
//else { return false; }