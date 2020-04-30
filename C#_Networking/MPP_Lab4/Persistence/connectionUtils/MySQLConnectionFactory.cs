using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace ConnectionUtils
{

    public class MySqlConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection()
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["db"];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return new MySqlConnection(returnValue);

        }
    }
}
