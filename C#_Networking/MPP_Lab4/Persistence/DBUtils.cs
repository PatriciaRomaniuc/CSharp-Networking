using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;

namespace Persistence
{
    public static class DBUtils
    {


        private static IDbConnection instance = null;

        public static IDbConnection getConnection()
        {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed)
            {
                instance = getNewConnection();
                instance.Open();
            }
            return instance;
        }

        private static IDbConnection getNewConnection()
        {

            return ConnectionUtils.ConnectionFactory.getInstance().createConnection();


        }
    }
}

