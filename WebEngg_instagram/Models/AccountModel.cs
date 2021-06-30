using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebEngg_instagram.Models
{
    public class AccountModel
    {
        private string connStr;
        public AccountModel()
        {
            connStr = "Data Source=DESKTOP-0RBAM18\\FA;Initial Catalog=Instagram;Persist Security Info=True;User ID=sa;Password=123";
        }

        public int ProfileValueUpdate(string name , string bio , string phone , string email , string city , string gender , string country , string dob ,string config)
        {
            string query = "update User_Info set Name = @name , Phone = @phone , Email =@email , city = @city , Country = @country , Gender = @gender , DOB = CONVERT (DATE, @dob), Bio = @bio where Username = @config";
            string Type = "TEXT";
            var parameters = new Dictionary<string, object>()
                {
                    { "@name", name },
                    { "@phone", phone },
                    { "@email", email },
                    { "@city", city },
                    { "@country", country },
                    { "@gender", gender },
                    { "@dob", dob },
                    { "@bio", bio },
                    { "@config", config },
                    { "ExampleOfNullParam", (object)DBNull.Value }
                };
            DatabaseHelper dh = new DatabaseHelper(connStr);
            return dh.UpdateOrDelete(query, Type , parameters);
        }
        public int CredentialValueUpdate(string oldpassword, string newpassword, string config)
        {
            int ret = 0;
            string Type = "TEXT";
            if (newpassword != "" && LegitPassword(oldpassword, config))
            {
                string query = "update Credential set Password = @password where Username = @user";
                var parameters = new Dictionary<string, object>()
                {
                    { "@password", newpassword },
                    { "@user", config },
                    { "ExampleOfNullParam", (object)DBNull.Value }
                };

                DatabaseHelper dh = new DatabaseHelper(connStr);
                return dh.UpdateOrDelete(query,Type, parameters);
            }
            return ret;

        }
        


    public bool LegitPassword(string password, string user)
        {
            //string query = "Select * from Credential where username = @user and password = @pass";
            string Type = "STOREDPROCEDURE";
            string Psname = "LoggingIn";
            DataTable dt = new DataTable();
            var parameters = new Dictionary<string, object>()
                {
                    { "@user", user },
                    { "pass", password }
                  //  { "ExampleOfNullParam", (object)DBNull.Value }
                };

            DatabaseHelper dh = new DatabaseHelper(connStr);
            dt =  dh.GetData(Psname, Type, parameters);
            if (dt.Rows.Count > 0)
            {
                if (password == dt.Rows[0]["password"].ToString())
                { return true; }
            }
            return false;
        }
        public DataTable DataBringer(string username)
        {
            //string query = "Select * from User_Info where username = @user";
            string PSname = "GetUserData";
            string Type = "STOREDPROCEDURE";

            DataTable dt = new DataTable();
            var parameters = new Dictionary<string, object>()
                {
                    { "@user", username },
                  //  { "ExampleOfNullParam", (object)DBNull.Value }
                };

            DatabaseHelper dh = new DatabaseHelper(connStr);
            if (username != "")
            {
                dt = dh.GetData(PSname, Type, parameters);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else { return dt; }
        }

        public bool LegitUsername(string username, string user)
        {
            int uncount = 0;
            string query = "select username from Credential";
            DataTable dt = new DataTable();
            SqlDataHelper sdh = new SqlDataHelper();

            dt = sdh.Select(query);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[0]["Username"].ToString() == username)
                    {
                        uncount++;
                    }
                }
            }
            if (uncount > 0) { return true; }
            else { return false; }
        }

    }
}