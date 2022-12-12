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
    public class PedidoRepository : IPedidoRepository
    {

        private readonly PedidoDao _pedidoDao;
        private readonly ClienteDao _clienteDao;
        private readonly ProdutoDao _produtoDao;
        private readonly Pedido _pedido;

        public PedidoRepository()
        {
            _pedidoDao = new PedidoDao();
            _clienteDao = new ClienteDao();
            _produtoDao = new ProdutoDao();
            _pedido = new Pedido();
        }

        public void AlteraStatusPedido(Pedido pedidoEditado)
        {
            var pedidoBuscado = _pedidoDao.BuscarPorId(pedidoEditado.IdPedido);
            if (pedidoBuscado == null)
            {
                throw new PedidoExcepition("Pedido não encontrado");
            }

            pedidoBuscado.StatusPedido = pedidoEditado.StatusPedido;
            _pedidoDao.EditarPedidoComCliente(pedidoBuscado);
            

        }

        public Pedido BuscarPedidoId(int idPedido)
        {
            var pedidoBuscado = _pedidoDao.BuscarPorId(idPedido);
            if (pedidoBuscado == null)
            {
                throw new PedidoExcepition("Pedido não encontrado");
            }
            return pedidoBuscado;
        }

        public List<Pedido> BuscarTodosPedidos()
        {
            var listaPedido = _pedidoDao.BuscarTodosPedido();
            if (listaPedido.Count == 0)
            {
                throw new PedidoExcepition("Nenhum pedido registrado");
            }
            return listaPedido;
        }

        public void CriarPedido(Pedido novoPedido)
        {
            var cliente = _clienteDao.BuscarPorCpf(novoPedido.ClienteId.Cpf);
            var produtoBuscado = _produtoDao.BuscarProdutoiD(novoPedido.ProdutoId.IdProduto);
            if (produtoBuscado == null)
            {
                throw new ProdutoException("Produto não encontrado");
            }
            else
            {
                if (produtoBuscado.Ativo == false)
                {
                    throw new ProdutoException("Produto não Desativado");
                }
                _pedido.Validar();
                
                if (produtoBuscado.Quantidade >= novoPedido.QuantidadeProduto)
                {
                    novoPedido.ProdutoId = produtoBuscado;

                    novoPedido.ValorTotal = novoPedido.CalcularValor();

                    produtoBuscado.Quantidade -= novoPedido.QuantidadeProduto;

                    _produtoDao.EditarProduto(produtoBuscado);

                    var clienteBuscado = _clienteDao.BuscarPorCpf(novoPedido.ClienteId.Cpf);
                    if (clienteBuscado == null)
                    {
                        novoPedido.ClienteId = null;

                        _pedidoDao.CriarPedidoSemCliente(novoPedido);

                    }
                    else
                    {
                        novoPedido.ClienteId = clienteBuscado;

                        novoPedido.ClienteId.Pontos += novoPedido.ValorTotal * 2;

                        _clienteDao.AdicionarPontosCliente(novoPedido.ClienteId);                       

                        _pedidoDao.CriarPedidoComCliente(novoPedido);
                    }

                    
                }
                else
                {
                    throw new ProdutoException("Não contém o produto suficiente no Estoque");
                }
            }            
        }

        public void DeletarPedido(int id)
        {
            var pedidoBuscado = _pedidoDao.BuscarPorId(id);
            if (pedidoBuscado == null)
            {
                throw new Exception("Pedido não Encontrado");
            }
            else
            {
                _pedidoDao.DeletarPedidoId(id);
            }
        }
    }
}