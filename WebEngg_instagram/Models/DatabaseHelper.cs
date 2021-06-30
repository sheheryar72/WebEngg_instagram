using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebEngg_instagram.Models
{
    public class DatabaseHelper
    {
        private string ConnString;

        public DatabaseHelper(string ConnectionString)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new ArgumentNullException();

            ConnString = ConnectionString;
        }

        public DataTable GetData(string query, string types, Dictionary<string, object> parameters = null )
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException();

            DataTable retVal = null;

            using (var connection = new SqlConnection(ConnString))
            {
                try
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        if (types == "TEXT")
                        { command.CommandType = CommandType.Text; }
                        if(types == "STOREDPROCEDURE")
                        { command.CommandType = CommandType.StoredProcedure;  }

                        if (parameters != null && parameters.Count > 0)
                            foreach (var p in parameters)
                                command.Parameters.AddWithValue(p.Key, p.Value);

                        using (var reader = command.ExecuteReader())
                        {
                            retVal = new DataTable();
                            retVal.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {   
                    connection.Close();
                }
            }

            return retVal;
        }

        // This method should be used when query returns more than one table
        public DataSet GetMultiTableData(string query, string types, Dictionary<string, object> parameters = null)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException();

            DataSet retVal = null;

            using (var connection = new SqlConnection(ConnString))
            {
                try
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        if (types == "TEXT")
                        { command.CommandType = CommandType.Text; }
                        if (types == "STOREDPROCEDURE")
                        { command.CommandType = CommandType.StoredProcedure; }

                        if (parameters != null && parameters.Count > 0)
                            foreach (var p in parameters)
                                command.Parameters.AddWithValue(p.Key, p.Value);

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(retVal);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return retVal;
        }

        public object Insert(string query, string types, Dictionary<string, object> parameters)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException();

            object retVal = null;

            using (var connection = new SqlConnection(ConnString))
            {
                try
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        if (types == "TEXT")
                        { command.CommandType = CommandType.Text; }
                        if (types == "STOREDPROCEDURE")
                        { command.CommandType = CommandType.StoredProcedure; }

                        if (parameters != null && parameters.Count > 0)
                            foreach (var p in parameters)
                                command.Parameters.AddWithValue(p.Key, p.Value);

                        retVal = command.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return retVal;
        }

        public int UpdateOrDelete(string query, string types, Dictionary<string, object> parameters)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException();

            int retVal = 0;

            using (var connection = new SqlConnection(ConnString))
            {
                try
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        if (types == "TEXT")
                        { command.CommandType = CommandType.Text; }
                        if (types == "STOREDPROCEDURE")
                        { command.CommandType = CommandType.StoredProcedure; }

                        if (parameters != null && parameters.Count > 0)
                            foreach (var p in parameters)
                                command.Parameters.AddWithValue(p.Key, p.Value);

                        retVal = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return retVal;
        }
    }
}