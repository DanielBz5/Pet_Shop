using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pet_Shop.Services;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Dao
{
    public class ConexaoBanco
    {
        public SqlConnection Open()
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var connectionString = configuration.GetConnectionString("DefaultConnection");

             SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            return connection;
        }

        public SqlConnection Connection()
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        public void Close()
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Close();
        }
    }
}
