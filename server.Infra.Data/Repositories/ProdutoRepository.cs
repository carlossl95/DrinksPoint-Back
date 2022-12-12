using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain;
using server.Domain.Exceptions;
using server.Domain.Repositories;
using server.Infra.Data.DAO;

namespace server.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly ProdutoDao _produtoDao;

        public ProdutoRepository()
        {
            _produtoDao = new ProdutoDao();
        }
        public void AdicionarProduto(Produto novoProduto)
        {
            novoProduto.Validar();
            if (novoProduto.Quantidade == null)
            {
                novoProduto.Quantidade = 0;
            }
            if (novoProduto.Ativo == null)
            {
                novoProduto.Ativo = true;
            }

            _produtoDao.CadastrarProduto(novoProduto);
        }

        public Produto BuscarProdutoId(int id)
        {
            var produtoBuscado = _produtoDao.BuscarProdutoiD(id);
            if (produtoBuscado == null)
                throw new ClienteException("Não existe este Id no Bando de Dados");
            
            return produtoBuscado;
            
        }

        public List<Produto> BuscarTodosProdutos()
        {
           var listaProdutos = _produtoDao.BuscarTodosProdutos();
           if (listaProdutos.Count == 0)
           {
                throw new Exception("Não existe nenhum registro!");
           }
           return listaProdutos;
        }

        public List<Produto> BuscarTodosProdutosAtivos()
        {
            var listaProdutos = _produtoDao.BuscarTodosProdutosAtivo();
            if (listaProdutos.Count == 0)
            {
                throw new Exception("Não existe nenhum Produto Ativo!");
            }
            return listaProdutos;
        }

        public void DeletarProdutoId(int id)
        {
            var clienteBuscado = _produtoDao.BuscarProdutoiD(id);
            if (clienteBuscado == null)
            {
                throw new Exception("Produto não encontrado");
            }            
            _produtoDao.DeletarProduto(id);            
        }

        public Produto EditarProduto(Produto produtoEditado)
        {
            var produtoBuscado = _produtoDao.BuscarProdutoiD(produtoEditado.IdProduto);
            if (produtoBuscado == null)
                throw new ClienteException("Não existe este Id no Bando de Dados");
            else
            {
                produtoBuscado.Descricao= produtoEditado.Descricao;
                produtoBuscado.Valor= produtoEditado.Valor;
                produtoBuscado.Validade= produtoEditado.Validade;
                _produtoDao.EditarProduto(produtoBuscado);
                return produtoBuscado;
            }
        }

        public void AtualizaEstoque(Produto produtoEditado)
        {
            var produtoBuscado = _produtoDao.BuscarProdutoiD(produtoEditado.IdProduto);
            if (produtoBuscado == null)
                throw new ClienteException("Não existe este Id no Bando de Dados");
            else
            {
                produtoBuscado.Quantidade += produtoEditado.Quantidade;
                _produtoDao.EditarProduto(produtoBuscado);
            }
        }

        public void AtivaDesativaProduto(Produto produtoEditado)
        {
            var produtoBuscado = _produtoDao.BuscarProdutoiD(produtoEditado.IdProduto);
            if (produtoBuscado == null)
                throw new ClienteException("Não existe este Id no Bando de Dados");
            else
            {
                if (produtoBuscado.Ativo == true)
                {
                    produtoBuscado.Ativo = false;
                }
                else
                {
                    produtoBuscado.Ativo = true;
                }

                _produtoDao.EditarProduto(produtoBuscado);
            }
            
        }


    }
}