using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Seva.DAL.Utilities
{
    public class MySqlFactory
    {

        public static MySqlConnection CreateConnection() => new MySqlConnection(DbConnection.DbConnectionString);

        public static MySqlCommand CreateCommand(string command, MySqlConnection connection) => new MySqlCommand(command, connection);

        public static MySqlDataAdapter CreateDataAdapter(MySqlCommand command) => new MySqlDataAdapter(command);
    }
}
