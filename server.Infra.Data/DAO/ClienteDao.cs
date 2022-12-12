using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain;
using System.Data.SqlClient;

namespace server.Infra.Data.DAO
{
    public class ClienteDao
    {
        private readonly string _connectionString = @"server=.\SQLEXPRESS;initial catalog=DRINKS_POINT_DB;integrated security=true;";

        public ClienteDao()
        {

        }

        internal Cliente Adicionar(Cliente novoCliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT CLIENTE 
                                     VALUES (@CPF
                                            ,@NOME
                                            ,@DATA_NASCIMENTO
                                            ,@PONTOS)";

                    comando.Parameters.AddWithValue("@CPF", novoCliente.Cpf);
                    comando.Parameters.AddWithValue("@NOME", novoCliente.Nome);
                    comando.Parameters.AddWithValue("@DATA_NASCIMENTO", novoCliente.DataNascimento);
                    comando.Parameters.AddWithValue("@PONTOS", novoCliente.Pontos);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            } 
           return novoCliente;
        }

        internal Cliente EditarCliente(Cliente clienteEditado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE CLIENTE 
                                        SET  CPF = @CPF
                                            ,NOME = @NOME                                            
                                            ,DATA_NASCIMENTO = @DATA_NASCIMENTO
                                            WHERE ID_CLIENTE = @ID_CLIENTE";

                    comando.Parameters.AddWithValue("ID_CLIENTE", clienteEditado.IdCliente);
                    comando.Parameters.AddWithValue("@CPF", clienteEditado.Cpf);
                    comando.Parameters.AddWithValue("@NOME", clienteEditado.Nome);
                    comando.Parameters.AddWithValue("@DATA_NASCIMENTO", clienteEditado.DataNascimento);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
            return clienteEditado;
        }

        internal Cliente AdicionarPontosCliente(Cliente cliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"update cliente 
                                        set  cpf = @cpf
                                            ,nome = @nome                                            
                                            ,data_nascimento = @data_nascimento
                                            ,pontos = @pontos
                                            where id_cliente = @id_cliente";

                    comando.Parameters.AddWithValue("id_cliente", cliente.IdCliente);
                    comando.Parameters.AddWithValue("@cpf", cliente.Cpf);
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@data_nascimento", cliente.DataNascimento);
                    comando.Parameters.AddWithValue("@pontos", cliente.Pontos);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
            return null;

        }

        internal List<Cliente> BuscarTodosClientes()
        {
            var listaCliente = new List<Cliente>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"SELECT ID_CLIENTE
                                         ,CPF
                                         ,NOME
                                         ,DATA_NASCIMENTO
                                         ,PONTOS
                                    FROM CLIENTE";

                    comando.CommandText = sql;

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var clienteBuscado = new Cliente
                        {
                            IdCliente = int.Parse(leitor["ID_CLIENTE"].ToString()),
                            Cpf = leitor["CPF"].ToString(),
                            Nome = leitor["NOME"].ToString(),
                            DataNascimento = DateTime.Parse(leitor["DATA_NASCIMENTO"].ToString()),
                            Pontos = decimal.Parse(leitor["PONTOS"].ToString()),
                        };

                        listaCliente.Add(clienteBuscado);
                    }
                }
            }

            return listaCliente;
        }

         internal void DeletarCliente(int id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"DELETE FROM CLIENTE WHERE ID_CLIENTE = @ID_CLIENTE";

                    comando.Parameters.AddWithValue("@ID_CLIENTE", id);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }            
        }


        internal Cliente BuscarPorCpf(string cpf)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"SELECT ID_CLIENTE
                                         ,CPF
                                         ,NOME
                                         ,DATA_NASCIMENTO
                                         ,PONTOS                                         
                                    FROM CLIENTE WHERE CPF = @CPF";

                    comando.CommandText = sql;

                    comando.Parameters.AddWithValue("@CPF", cpf);

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var clienteBuscado = new Cliente
                        {
                            IdCliente = int.Parse(leitor["ID_CLIENTE"].ToString()),
                            Cpf = leitor["CPF"].ToString(),
                            Nome = leitor["NOME"].ToString(),
                            DataNascimento = DateTime.Parse(leitor["DATA_NASCIMENTO"].ToString()),
                            Pontos = long.Parse(leitor["PONTOS"].ToString()),                           
                        };
                        return clienteBuscado;
                    }
                }
            }
            return null;
        }

        internal Cliente BuscarPorId(int idCliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"SELECT ID_CLIENTE
                                         ,CPF
                                         ,NOME
                                         ,DATA_NASCIMENTO
                                         ,PONTOS                                         
                                    FROM CLIENTE WHERE ID_CLIENTE = @ID_CLIENTE";

                    comando.CommandText = sql;

                    comando.Parameters.AddWithValue("@ID_CLIENTE", idCliente);

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var clienteBuscado = new Cliente
                        {
                            IdCliente = int.Parse(leitor["ID_CLIENTE"].ToString()),
                            Cpf = leitor["CPF"].ToString(),
                            Nome = leitor["NOME"].ToString(),
                            DataNascimento = DateTime.Parse(leitor["DATA_NASCIMENTO"].ToString()),
                            Pontos = decimal.Parse(leitor["PONTOS"].ToString()),                           
                        };
                        return clienteBuscado;
                    }
                }
            }
            return null;
        }





    }
}