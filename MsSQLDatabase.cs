using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace AspNet.Identity.ServiceStack
{
    /// <summary>
    /// Class that encapsulates a MsSQL database connections 
    /// and CRUD operations
    /// </summary>
    public class MsSQLDatabase
    {
        private static OrmLiteConnectionFactory _connectionFactory;

        /// Default constructor which uses the "DefaultConnection" connectionString
        /// </summary>
        public MsSQLDatabase()
            : this("DefaultConnection")
        {
        }

        /// <summary>
        /// Constructor which takes the connection string name
        /// </summary>
        /// <param name="connectionStringName"></param>
        public MsSQLDatabase(string connectionStringName)
        {
            if (_connectionFactory != null)
                return;

            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            _connectionFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
        }

        public IDbConnection Open()
        {
            return _connectionFactory.OpenDbConnection();
        }
    }
}
