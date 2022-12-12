using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using server.Domain;

namespace server.Infra.Data.DAO
{
    public class ProdutoDao
    {
        private readonly string _connectionString = @"server=.\SQLEXPRESS;initial catalog=DRINKS_POINT_DB;integrated security=true;";

        public ProdutoDao()
        {

        }

        internal List<Produto> BuscarTodosProdutos()
        {
            var listaProduto = new List<Produto>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"SELECT ID_PRODUTO
                                         ,DESCRICAO
                                         ,QUANTIDADE
                                         ,VALOR
                                         ,VALIDADE
                                         ,ATIVO
                                    FROM PRODUTO";

                    comando.CommandText = sql;

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var produtoBuscado = new Produto
                        {
                            IdProduto = int.Parse(leitor["ID_PRODUTO"].ToString()),
                            Descricao = leitor["DESCRICAO"].ToString(),
                            Quantidade = int.Parse(leitor["QUANTIDADE"].ToString()),
                            Valor = decimal.Parse(leitor["VALOR"].ToString()),
                            Validade = DateTime.Parse(leitor["VALIDADE"].ToString()),
                            Ativo = Boolean.Parse(leitor["ATIVO"].ToString())
                        };

                        listaProduto.Add(produtoBuscado);
                    }
                }
            }

            return listaProduto;
        }

        internal List<Produto> BuscarTodosProdutosAtivo()
        {
            var listaProduto = new List<Produto>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"SELECT ID_PRODUTO
                                         ,DESCRICAO
                                         ,QUANTIDADE
                                         ,VALOR
                                         ,VALIDADE
                                         ,ATIVO
                                    FROM PRODUTO";

                    comando.CommandText = sql;

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var produtoBuscado = new Produto
                        {
                            IdProduto = int.Parse(leitor["ID_PRODUTO"].ToString()),
                            Descricao = leitor["DESCRICAO"].ToString(),
                            Quantidade = int.Parse(leitor["QUANTIDADE"].ToString()),
                            Valor = decimal.Parse(leitor["VALOR"].ToString()),
                            Validade = DateTime.Parse(leitor["VALIDADE"].ToString()),
                            Ativo = Boolean.Parse(leitor["ATIVO"].ToString())
                        };
                        if (produtoBuscado.Ativo == true)
                        {
                            listaProduto.Add(produtoBuscado);
                        }                        
                    }
                }
            }

            return listaProduto;
        }

        internal Produto BuscarProdutoiD(int idProduto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"SELECT ID_PRODUTO
                                         ,DESCRICAO
                                         ,QUANTIDADE
                                         ,VALOR
                                         ,VALIDADE
                                         ,ATIVO
                                    FROM PRODUTO WHERE ID_PRODUTO = @ID_PRODUTO";

                    comando.CommandText = sql;

                    comando.Parameters.AddWithValue("@ID_PRODUTO", idProduto);

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var ProdutoBuscado = new Produto
                        {
                            IdProduto = int.Parse(leitor["ID_PRODUTO"].ToString()),
                            Descricao = leitor["DESCRICAO"].ToString(),
                            Quantidade = int.Parse(leitor["QUANTIDADE"].ToString()),
                            Valor = decimal.Parse(leitor["VALOR"].ToString()),
                            Validade = DateTime.Parse(leitor["VALIDADE"].ToString()),
                            Ativo = Boolean.Parse(leitor["ATIVO"].ToString())
                        };

                        return ProdutoBuscado;
                    }
                }
            }

            return null;
        }

        internal void CadastrarProduto(Produto novoProduto)
        {
            
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT PRODUTO 
                                     VALUES (@DESCRICAO
                                            ,@QUANTIDADE
                                            ,@VALOR
                                            ,@VALIDADE
                                            ,@ATIVO)";

                    comando.Parameters.AddWithValue("@DESCRICAO", novoProduto.Descricao);
                    comando.Parameters.AddWithValue("@QUANTIDADE", novoProduto.Quantidade);
                    comando.Parameters.AddWithValue("@VALOR", novoProduto.Valor);
                    comando.Parameters.AddWithValue("@VALIDADE", novoProduto.Validade);
                    comando.Parameters.AddWithValue("@ATIVO", novoProduto.Ativo);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        internal Produto EditarProduto(Produto produtoEditado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PRODUTO 
                                        SET DESCRICAO = @DESCRICAO
                                           ,QUANTIDADE = @QUANTIDADE
                                           ,VALOR = @VALOR
                                           ,VALIDADE = @VALIDADE
                                           ,ATIVO = @ATIVO
                                           WHERE ID_PRODUTO = @ID_PRODUTO";

                    comando.Parameters.AddWithValue("@ID_PRODUTO", produtoEditado.IdProduto);
                    comando.Parameters.AddWithValue("@DESCRICAO", produtoEditado.Descricao);
                    comando.Parameters.AddWithValue("@QUANTIDADE", produtoEditado.Quantidade);
                    comando.Parameters.AddWithValue("@VALOR", produtoEditado.Valor);
                    comando.Parameters.AddWithValue("@VALIDADE", produtoEditado.Validade);
                    comando.Parameters.AddWithValue("@ATIVO", produtoEditado.Ativo);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
            return produtoEditado;
        }       

        internal void DeletarProduto(int id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;

                    string sql = @"DELETE FROM PRODUTO WHERE ID_PRODUTO = @ID_PRODUTO";

                    comando.Parameters.AddWithValue("@ID_PRODUTO", id);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }      
        }
    }
        
}