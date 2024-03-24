using MySql.Data.MySqlClient;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Dao
{
    public class AgendamentoDao
    {
        ConexaoBanco conexaobanco;
        public AgendamentoDao()
        {
            conexaobanco = new ConexaoBanco();
        }


        public List<Servicos> ListaServicos()
        {
            List<Servicos> listaservico = new List<Servicos>();
            try
            {
                string sql = "SELECT * FROM servicos;";

                MySqlConnection connection = conexaobanco.Open();

                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
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

        public Servicos BuscaServico(int CodServico)
        {
            Servicos servico = new Servicos();
            try
            {
                string sql = "SELECT * FROM servicos WHERE Cod = '"+ CodServico + "';";

                MySqlConnection connection = conexaobanco.Open();

                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        servico.Cod = Convert.ToInt32(reader["Cod"]);
                        servico.Nome = reader["Nome"].ToString();
                        servico.Valor = Convert.ToInt32(reader["Valor"]);
                        servico.Descricao = reader["Descricao"].ToString();
                    }
                }

                return servico;
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

        public Tuple<Cliente, Pet> BuscaClientePet(Cliente cliente)
        {
            try
            {
                string sql = "SELECT * FROM clientes WHERE Nome = '" + cliente.Nome + "' AND Senha = '" + cliente.Senha + "';";

                MySqlConnection connection = conexaobanco.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cliente.Cpf = Convert.ToString(reader["CPF"]);
                        cliente.Nome = Convert.ToString(reader["Nome"]);
                        cliente.Senha = Convert.ToString(reader["Senha"]);
                        cliente.Telefone = Convert.ToString(reader["Telefone"]);
                        cliente.Endereco = Convert.ToString(reader["Endereco"]);
                    }
                }

                //Busca Pet do Cliente 

                Pet pet = new Pet();

                sql = "SELECT * FROM pet WHERE CPF_Dono = '" + cliente.Cpf + "';";
                command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pet.CpfDono = Convert.ToString(reader["CPF_Dono"]);
                        pet.Nome = Convert.ToString(reader["Nome"]);
                        pet.Especie = Convert.ToString(reader["Especie"]);
                        pet.Raca = Convert.ToString(reader["Raca"]);
                    }
                }

                return new Tuple<Cliente, Pet>(cliente, pet);
            }
            catch
            {
                return null;
            }
            finally
            {
                conexaobanco.Close();
            }
        }

        public bool IncluiAgendamento(Cliente cliente, Pet pet, Servicos servicos, Agendamento agendamento)
        {

            try
            {
                string sql = "SELECT * FROM servicos WHERE Cod ='" + servicos.Cod + "';";
                MySqlConnection connection = conexaobanco.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        servicos.Cod = Convert.ToInt32(reader["Cod"]);
                        servicos.Nome = Convert.ToString(reader["Nome"]);
                        servicos.Descricao = Convert.ToString(reader["Descricao"]);
                        servicos.Valor = Convert.ToInt32(reader["Valor"]);
                    }
                }


                sql = "INSERT INTO agendamento (CPF, Nome_Cliente, Telefone, Nome_Pet, Especie, Nome_Servico, _Data)" +
                       "VALUES ('" + cliente.Cpf + "','" + cliente.Nome + "','" + cliente.Telefone + "','" + pet.Nome + "'" +
                       ",'" + pet.Especie + "','" + servicos.Nome + "','" + agendamento.Data + "');";


                command = new MySqlCommand(sql, connection);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                conexaobanco.Close();
            }
        }


        public List<Agendamento> ListaAgenda()
        {
            List<Agendamento> ListaAgenda = new List<Agendamento>();

            try
            {
                string sql = "SELECT * FROM agendamento;";

                MySqlConnection connection = conexaobanco.Open();

                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Agendamento agendamento = new Agendamento
                        {
                            Cod = Convert.ToInt32(reader["Cod"]),
                            Cpf = reader["CPF"].ToString(),
                            Nome_Cliente = reader["Nome_Cliente"].ToString(),
                            Telefone = reader["Telefone"].ToString(),
                            Nome_Pet = reader["Nome_Pet"].ToString(),
                            Especie = reader["Especie"].ToString(),
                            Nome_Servico = reader["Nome_Servico"].ToString(),
                            Data = Convert.ToDateTime(reader["_Data"]),
                        };

                        ListaAgenda.Add(agendamento);
                    }
                }

                return ListaAgenda;
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

        public List<Agendamento> FiltraAgenda(Agendamento agendamento)
        {
            List<Agendamento> ListaAgenda = new List<Agendamento>();
            string Data = agendamento.Data.ToString("dd/MM/yyyy");
            if (agendamento.Cpf != null && Data == "01/01/0001")
            {
                Data = "*";
            }
            else if (agendamento.Cpf != null && Data != null)
            {
                Data = agendamento.Data.ToString("dd/MM/yyyy");
            }

            try
            {
                string sql = "SELECT * FROM agendamento WHERE CPF = '" + agendamento.Cpf + "' OR _Data like '%" + Data + "%';";

                MySqlConnection connection = conexaobanco.Open();

                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Agendamento agendamento1 = new Agendamento
                        {
                            Cod = Convert.ToInt32(reader["Cod"]),
                            Cpf = reader["CPF"].ToString(),
                            Nome_Cliente = reader["Nome_Cliente"].ToString(),
                            Telefone = reader["Telefone"].ToString(),
                            Nome_Pet = reader["Nome_Pet"].ToString(),
                            Especie = reader["Especie"].ToString(),
                            Nome_Servico = reader["Nome_Servico"].ToString(),
                            Data = Convert.ToDateTime(reader["_Data"]),
                        };

                        ListaAgenda.Add(agendamento1);
                    }
                }

                return ListaAgenda;
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

        public List<Agendamento> AgendaCliente(Cliente cliente)
        {
            List<Agendamento> ListaAgenda = new List<Agendamento>();

            try
            {
                string sql = "SELECT * FROM clientes WHERE Nome = '" + cliente.Nome + "' AND Senha = '" + cliente.Senha + "';";

                MySqlConnection connection = conexaobanco.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cliente.Cpf = Convert.ToString(reader["CPF"]);
                        cliente.Nome = Convert.ToString(reader["Nome"]);
                        cliente.Senha = Convert.ToString(reader["Senha"]);
                        cliente.Telefone = Convert.ToString(reader["Telefone"]);
                        cliente.Endereco = Convert.ToString(reader["Endereco"]);
                    }
                }

                sql = "SELECT * FROM agendamento WHERE CPF = '" + cliente.Cpf + "';";

                connection = conexaobanco.Open();

                command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Agendamento agendamento = new Agendamento
                        {
                            Cod = Convert.ToInt32(reader["Cod"]),
                            Cpf = reader["CPF"].ToString(),
                            Nome_Cliente = reader["Nome_Cliente"].ToString(),
                            Telefone = reader["Telefone"].ToString(),
                            Nome_Pet = reader["Nome_Pet"].ToString(),
                            Especie = reader["Especie"].ToString(),
                            Nome_Servico = reader["Nome_Servico"].ToString(),
                            Data = Convert.ToDateTime(reader["_Data"]),
                        };

                        ListaAgenda.Add(agendamento);
                    }
                }

                return ListaAgenda;
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

        public Cliente BuscaCliente(Cliente cliente)
        {
            try
            {
                string sql = "SELECT * FROM clientes WHERE Nome = '" + cliente.Nome + "' AND Senha = '" + cliente.Senha + "';";

                MySqlConnection connection = conexaobanco.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cliente.Cpf = Convert.ToString(reader["CPF"]);
                        cliente.Nome = Convert.ToString(reader["Nome"]);
                        cliente.Senha = Convert.ToString(reader["Senha"]);
                        cliente.Telefone = Convert.ToString(reader["Telefone"]);
                        cliente.Endereco = Convert.ToString(reader["Endereco"]);
                    }
                }

                return cliente;
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

        public Agendamento BuscaAgendamento(Agendamento agendamento)
        {
            try
            {
                string sql = "SELECT * FROM agendamento WHERE Cod = '"+agendamento.Cod+"' ;";

                MySqlConnection connection = conexaobanco.Open();

                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        agendamento.Cod = Convert.ToInt32(reader["Cod"]);
                        agendamento.Cpf = reader["CPF"].ToString();
                        agendamento.Nome_Cliente = reader["Nome_Cliente"].ToString();
                        agendamento.Telefone = reader["Telefone"].ToString();
                        agendamento.Nome_Pet = reader["Nome_Pet"].ToString(); ;
                        agendamento.Especie = reader["Especie"].ToString();
                        agendamento.Nome_Servico = reader["Nome_Servico"].ToString();
                        agendamento.Data = Convert.ToDateTime(reader["_Data"]);
                    }
                }

                return agendamento;
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

        public bool DeleteAgendamento(Agendamento agendamento)
        {
            try
            {
                string sql = "DELETE FROM agendamento WHERE Cod = '" + agendamento.Cod + "';";
               
                MySqlConnection connection = conexaobanco.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
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
