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

        public bool logincheck(string username ,string password)
        {
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand cmd = connection.CreateCommand();
            string query = "Select * from Credential where username = @user";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@user", username);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            //connection.Close();
            DataTable dt = new DataTable(reader.ToString());
            dt.Load(reader);

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
    }
}

//string sqlq = "Select * from Credential where username = '"+username+"'";
//SqlCommand cmd = new SqlCommand(sqlq , connection);
//SqlDataReader sdr = cmd.ExecuteReader();
//DataTable dt = new DataTable();
//dt.Load(sdr);