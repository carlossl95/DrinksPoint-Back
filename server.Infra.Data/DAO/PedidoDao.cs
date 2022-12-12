using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using server.Domain;

namespace server.Infra.Data.DAO
{
    public class PedidoDao
    {

        private readonly string _connectionString = @"server=.\SQLEXPRESS;initial catalog=DRINKS_POINT_DB;integrated security=true;";

        public PedidoDao()
        {
        }
        internal Pedido BuscarPorId(int idPedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"SELECT  P.ID_PEDIDO
                                            ,P.VALOR_TOTAL
                                            ,P.STATUS_PEDIDO
                                            ,P.DATA_PEDIDO
                                            ,C.NOME
                                            FROM PEDIDO P
                                            INNER JOIN CLIENTE C
                                            ON C.ID_CLIENTE = P.CLIENTE_ID
                                            WHERE P.ID_PEDIDO = @ID_PEDIDO";

                    comando.CommandText = sql;

                    comando.Parameters.AddWithValue("@ID_PEDIDO", idPedido);

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Pedido pedidoBuscado = new Pedido();

                        pedidoBuscado.IdPedido = int.Parse(leitor["ID_PEDIDO"].ToString());
                        pedidoBuscado.DataPedido = DateTime.Parse(leitor["DATA_PEDIDO"].ToString());
                        pedidoBuscado.ValorTotal = decimal.Parse(leitor["VALOR_TOTAL"].ToString());
                        pedidoBuscado.StatusPedido = leitor["STATUS_PEDIDO"].ToString();
                        pedidoBuscado.ClienteId = new Cliente()
                        {
                            Nome = leitor["NOME"].ToString()
                        };
                        return pedidoBuscado;
                    }
                }
            }
            return null;
        }

        internal List<Pedido> BuscarTodosPedido()
        {
            var listaPedido = new List<Pedido>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"SELECT  P.ID_PEDIDO
                                            ,P.VALOR_TOTAL
                                            ,P.STATUS_PEDIDO
                                            ,P.DATA_PEDIDO
                                            ,C.NOME
                                            FROM PEDIDO P
                                            INNER JOIN CLIENTE C
                                            ON C.ID_CLIENTE = P.CLIENTE_ID";

                    comando.CommandText = sql;

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Pedido pedidoBuscado = new Pedido();

                        pedidoBuscado.IdPedido = int.Parse(leitor["ID_PEDIDO"].ToString());
                        pedidoBuscado.DataPedido = DateTime.Parse(leitor["DATA_PEDIDO"].ToString());
                        pedidoBuscado.ValorTotal = decimal.Parse(leitor["VALOR_TOTAL"].ToString());
                        pedidoBuscado.StatusPedido = leitor["STATUS_PEDIDO"].ToString();
                        pedidoBuscado.ClienteId = new Cliente()
                        {
                            Nome = leitor["NOME"].ToString()
                        };
                        listaPedido.Add(pedidoBuscado);
                    }
                }
            }
            return listaPedido;
        }

        internal void CriarPedidoComCliente(Pedido novoPedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT PEDIDO 
                                     VALUES (@CLIENTE_ID
                                            ,@PRODUTO_ID
                                            ,@DATA_PEDIDO
                                            ,@QUANTIDADE_PRODUTO
                                            ,@VARLOR_TOTAL
                                            ,@STATUS_PEDIDO)";

                    comando.Parameters.AddWithValue("@CLIENTE_ID", novoPedido.ClienteId.IdCliente);
                    comando.Parameters.AddWithValue("@PRODUTO_ID", novoPedido.ProdutoId.IdProduto);
                    comando.Parameters.AddWithValue("@DATA_PEDIDO", novoPedido.DataPedido);
                    comando.Parameters.AddWithValue("@QUANTIDADE_PRODUTO", novoPedido.QuantidadeProduto);
                    comando.Parameters.AddWithValue("@VARLOR_TOTAL", novoPedido.ValorTotal);
                    comando.Parameters.AddWithValue("@STATUS_PEDIDO", novoPedido.StatusPedido);
                    
                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        internal void CriarPedidoSemCliente(Pedido novoPedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT PEDIDO 
                                     VALUES (
                                            @PRODUTO_ID
                                            ,@DATA_PEDIDO
                                            ,@QUANTIDADE_PRODUTO
                                            ,@VARLOR_TOTAL
                                            ,@STATUS_PEDIDO)";

                    
                    comando.Parameters.AddWithValue("@PRODUTO_ID", novoPedido.ProdutoId.IdProduto);
                    comando.Parameters.AddWithValue("@DATA_PEDIDO", novoPedido.DataPedido);
                    comando.Parameters.AddWithValue("@QUANTIDADE_PRODUTO", novoPedido.QuantidadeProduto);
                    comando.Parameters.AddWithValue("@VARLOR_TOTAL", novoPedido.ValorTotal);
                    comando.Parameters.AddWithValue("@STATUS_PEDIDO", novoPedido.StatusPedido);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        internal void EditarPedidoComCliente(Pedido novoPedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PEDIDO 
                                        SET  STATUS_PEDIDO = @STATUS_PEDIDO
                                             WHERE ID_PEDIDO = @ID_PEDIDO";

                    comando.Parameters.AddWithValue("@ID_PEDIDO", novoPedido.IdPedido);
                    comando.Parameters.AddWithValue("@STATUS_PEDIDO", novoPedido.StatusPedido);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        internal void DeletarPedidoId(int idPedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"DELETE FROM PEDIDO WHERE ID_PEDIDO = @ID_PEDIDO";

                    comando.Parameters.AddWithValue("@ID_PEDIDO", idPedido);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        internal void DeletarPedidoIdCliente(int idCliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"DELETE FROM PEDIDO WHERE CLIENTE_ID = @CLIENTE_ID";

                    comando.Parameters.AddWithValue("@CLIENTE_ID", idCliente);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}