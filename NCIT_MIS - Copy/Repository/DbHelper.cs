using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class DbHelper
    {
        public static SqlConnection GetDBConnection()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ncit_mis"].ToString();
            var connection = new SqlConnection(connectionString.ToString());
            connection.Open();
            return connection;
        }
    }
}