using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebEngg_instagram.Models
{
    public class SqlDataHelper
    {
        public String ConnectionString = "Data Source=DESKTOP-0RBAM18\\FA;Initial Catalog = Instagram; Persist Security Info=True;User ID = sa; Password=123";
        public SqlConnection connection;
        public SqlDataHelper()
        {
            ConnectionString = ReadConnectionString();
            connection = new SqlConnection(ConnectionString);
        }
        public String ReadConnectionString()
        {
            return ConnectionString = "Data Source=DESKTOP-0RBAM18\\FA;Initial Catalog = Instagram; Persist Security Info=True;User ID = sa; Password=123";
        }
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
            }
            return false;
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        public DataTable Select(String Querys)
        {

            DataTable dt = new DataTable();
            if (OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(Querys, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                CloseConnection();
            }
            return dt;
        }
        public int Ins_upd_del(String Querys)
        {
            int result = 0;
            if (OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(Querys, connection);
                result = cmd.ExecuteNonQuery();
                CloseConnection();
            }
            return result;
        }
        public DataTable Select(String Querys, params SqlParameter[] command_parameter)
        {

            DataTable dt = new DataTable();
            if (OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(Querys, connection);
                AssignParameterValues(command_parameter, command_parameter);
                AttachParameters(cmd, command_parameter);

                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                CloseConnection();
            }
            return dt;
        }

        public int Ins_upd_del(String Querys, params SqlParameter[] command_parameter)
        {
            int result = 0;
            if (OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(Querys, connection);
                AssignParameterValues(command_parameter, command_parameter);
                AttachParameters(cmd, command_parameter);
                result = cmd.ExecuteNonQuery();
                CloseConnection();
            }
            return result;
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned  
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                return;
            }
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                if (parameterValues[i] is IDbDataParameter)
                {
                    IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                    if (paramInstance.Value == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if (parameterValues[i] == null)
                {
                    //commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }
    }
}
