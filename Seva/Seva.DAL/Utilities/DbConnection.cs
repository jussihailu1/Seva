using System;
using System.Collections.Generic;
using System.Text;

namespace Seva.DAL.Utilities
{
    public class DbConnection
    {
        public static string DbConnectionString { get; private set; }

        public static void SetConnectionString(string connectionString)
        {
            DbConnectionString = connectionString;
        }
    }
}
