using EduBIN.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.Utility.Database
{
    public static class DBConnection
    {
        private static SqlConnection SqlConnection;
        private static readonly string ConnectionString = ConfigureAppSettings.Configure.GetSection("Connection")["ConnectionString"];

        public static SqlConnection GetConnection()
        {
            if (SqlConnection == null)
            {
                try
                {
                    return new SqlConnection(ConnectionString);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
            return SqlConnection;
        }
    }
}