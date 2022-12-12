using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Repositories
{
    public interface IProdutoRepository
    {
        void AdicionarProduto(Produto novoProduto);
        Produto BuscarProdutoId(int id);
        List<Produto> BuscarTodosProdutos();
        List<Produto> BuscarTodosProdutosAtivos();
        void AtualizaEstoque(Produto produtoEditado);
        void DeletarProdutoId(int id);
        Produto EditarProduto(Produto produtoEditado);
        void AtivaDesativaProduto(Produto produtoEditado);
    }
}