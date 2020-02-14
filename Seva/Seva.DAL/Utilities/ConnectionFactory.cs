using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Seva.DAL.Utilities
{
    public class ConnectionFactory
    {
        public static void SetConnectionString(string connectionString) => DbConnection.SetConnectionString(connectionString);
    }
}
