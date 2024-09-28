using Microsoft.Data.SqlClient;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Dao
{
    public class OrcamentoDao
    {
        ConexaoBanco conexaobanco;
        public OrcamentoDao()
        {
            conexaobanco = new ConexaoBanco();
        }

        public List<Servicos> ListaServicos()
        {
            List<Servicos> listaservico = new List<Servicos>();
            try
            {
                string sql = "SELECT * FROM servicos;";

                SqlConnection connection = conexaobanco.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Servicos servico = new Servicos
                        {
                            Cod = Convert.ToInt32(reader["Cod"]),
                            Nome = reader["Nome"].ToString(),
                            Valor = Convert.ToInt32(reader["Valor"]),
                            Descricao = reader["Descricao"].ToString()
                        };

                        listaservico.Add(servico);
                    }
                }

                return listaservico;
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
    }
}
