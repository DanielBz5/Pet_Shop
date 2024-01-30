using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
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
        public MySqlConnection Open()
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var connectionString = configuration.GetConnectionString("DefaultConnection");

             MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            return connection;
        }

        public MySqlConnection Connection()
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            MySqlConnection connection = new MySqlConnection(connectionString);

            return connection;
        }

        public void Close()
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Close();
        }
    }
}
