using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PrimeApps_Beta.Gateway
{
    public class DBConnector
    {
        private SqlConnection connection;

        string connectionString;

        public DBConnector()
        {
            connectionString = @"Data Source=192.168.150.6\SQLEXPRESS;Initial Catalog=PrimeTexLIVE;Persist Security Info=True;User ID=dev;Password=321";
            //connectionString = @"Data Source=192.168.150.127\SQLEXPRESS;Initial Catalog=PrimeTexLIVE;Persist Security Info=True;User ID=prime;Password=123";
        }

        public SqlConnection Connection()
        {
            return new SqlConnection(connectionString);
        }

        public DataTable ExecuteQueryDT(string sqlQuery)
        {
            DataTable dt = new DataTable();
            using (connection = Connection())
            {
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, connection);
                da.Fill(dt);
            }

            return dt;
        }

        public DataSet ExecuteQueryDS(string sqlQuery)
        {
            DataSet ds = new DataSet();
            using (connection = Connection())
            {

                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, connection);
                da.Fill(ds);
            }

            return ds;
        }

        public int ExecuteNonQuery(string sqlQuery)
        {
            int res = -1;
            using (connection = Connection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                res = cmd.ExecuteNonQuery();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return res;
        }

        public object ExecuteScalar(string sqlQuery)
        {
            object res;

            using (connection = Connection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                res = cmd.ExecuteScalar();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return res;
        }

    }
}
