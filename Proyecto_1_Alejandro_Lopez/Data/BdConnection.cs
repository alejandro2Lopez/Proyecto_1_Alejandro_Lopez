using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_1_Alejandro_Lopez.Data
{
    public class BdConnection
    {
        private readonly string StringConnection;

        public BdConnection()
        {
            StringConnection = ConfigurationManager
                .ConnectionStrings["ConexionBD"]
                .ConnectionString;
        }

        public SqlConnection getConnection()
        {
            return new SqlConnection(StringConnection);
        }
    }
}