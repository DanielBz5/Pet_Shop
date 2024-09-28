using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Dao
{
    public class HomeDao
    {
        ConexaoBanco conexaobanco;
   
        public HomeDao()
        {
            conexaobanco = new ConexaoBanco();
        }

        public Cliente ValidaLogin(Cliente cliente)
        {
            try { 
                string sql = "SELECT * FROM clientes WHERE Nome = '" + cliente.Nome + "' and Senha = '" + cliente.Senha + "'";

                SqlConnection connection = conexaobanco.Open();
            
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cliente.Nome = Convert.ToString(reader["Nome"]);
                        cliente.Senha = Convert.ToString(reader["Senha"]);
                        return cliente;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                conexaobanco.Close();
            }
        }

        public bool IncluiRegister(Cliente cliente, Pet pet)
        {
            try
            {
                string sql = "INSERT INTO clientes (CPF, Nome, Senha, Telefone, Endereco, RememberMe) " +
                             "VALUES ('"+cliente.Cpf +"','"+cliente.Nome + "','" + cliente.Senha + "','" + cliente.Telefone + "','" + cliente.Endereco+"','0');";

                sql = sql + "INSERT INTO pet (CPF_Dono, Nome, Especie, Raca) " +
                          "VALUES ('" + cliente.Cpf + "','" + pet.Nome + "','" + pet.Especie + "','" + pet.Raca +"');";

                SqlConnection connection = conexaobanco.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                int rowsAffected = command.ExecuteNonQuery();

                if(rowsAffected > 0) 
                {
                    return true; 
                }
                else 
                {
                    return false;
                }
                    
            }
            catch(Exception)
            {
                return false;
            }
            finally
            {
                conexaobanco.Close();
            }
        }
    }
}
