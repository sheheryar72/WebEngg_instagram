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
        public AccountModel()
        {

        }

        public int ProfileValueUpdate(string name , string bio , string phone , string email , string city , string gender , string country , string dob ,string config)
        {
            string query = "update User_Info set Name = @name , Phone = @phone , Email =@email , city = @city , Country = @country , Gender = @gender , DOB = CONVERT (DATE, @dob), Bio = @bio where Username = @config";
            SqlDataHelper sdh = new SqlDataHelper();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@name", name);
            param[1] = new SqlParameter("@bio", bio);
            param[2] = new SqlParameter("@phone", phone);
            param[3] = new SqlParameter("@email", email);
            param[4] = new SqlParameter("@city", city);
            param[5] = new SqlParameter("@gender", gender);
            param[6] = new SqlParameter("@country", country);
            param[7] = new SqlParameter("@dob", dob);
            param[8] = new SqlParameter("@config", config);
            int ret = sdh.Ins_upd_del(query, param);

            return ret;
        }
        public int CredentialValueUpdate(string oldpassword, string newpassword, string config)
        {
            int ret = 0;
            if (newpassword != "" && LegitPassword(oldpassword, config))
            {
                string query = "update Credential set Password = @password where Username = @user";
                SqlDataHelper sdh = new SqlDataHelper();
                SqlParameter[] paramm = new SqlParameter[2];
                paramm[0] = new SqlParameter("@password", newpassword);
                paramm[1] = new SqlParameter("@user", config);
                ret = sdh.Ins_upd_del(query, paramm);
            }
            return ret;

        }
        


    public bool LegitPassword(string password, string user)
        {
            string query = "select password from Credential where username = @user";
            DataTable dt = new DataTable();
            SqlDataHelper sdh = new SqlDataHelper();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("user", user);
            dt = sdh.Select(query,param);
            if (dt.Rows.Count > 0)
            {
                if (password == dt.Rows[0]["password"].ToString())
                { return true; }
            }
            return false;
        }
        public DataTable DataBringer(string username)
        {
            string Userinfo = "Select * from User_Info where username = @user";

            DataTable dt = new DataTable();
            SqlDataHelper sdh = new SqlDataHelper();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@user", username);
            if (username != "")
            {
                dt = sdh.Select(Userinfo, param);
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