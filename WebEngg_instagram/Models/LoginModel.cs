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
            string SP = "LoggingIn";
            string Type = "STOREDPROCEDURE";
            DataTable dt = new DataTable();
            var parameters = new Dictionary<string, object>()
                {
                    { "user", username },
                    { "pass", password },
                  //  { "ExampleOfNullParam", (object)DBNull.Value }
                };

            DatabaseHelper dh = new DatabaseHelper(connStr);

            dt = dh.GetData(SP , Type , parameters);

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

        public bool CheckUser(string username)
        {
            string SP = "GetUserData";
            string Type = "STOREDPROCEDURE";
            DataTable dt = new DataTable();
            var parameters = new Dictionary<string, object>()
                {
                    { "user", username }
                  //  { "ExampleOfNullParam", (object)DBNull.Value }
                };

            DatabaseHelper dh = new DatabaseHelper(connStr);

            dt = dh.GetData(SP, Type, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else { return false; }
        }
        public bool SigningUp(string name , string email , string username , string password)
        {
            string SP = "InsertUser";
            string Type = "STOREDPROCEDURE";
            DataTable dt = new DataTable();
            var parameters = new Dictionary<string, object>()
                {
                    { "name", name },
                    { "email", email},
                    { "username", username},
                    { "password", password}
                  //  { "ExampleOfNullParam", (object)DBNull.Value }
                };

            DatabaseHelper dh = new DatabaseHelper(connStr);

            dt = dh.GetData(SP, Type, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else { return false; }
        }
    }
}
