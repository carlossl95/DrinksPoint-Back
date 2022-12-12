using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Repositories
{
    public interface IPedidoRepository
    {
        void AlteraStatusPedido(Pedido pedidoEditado);
       Pedido BuscarPedidoId(int idPedido);
       List<Pedido> BuscarTodosPedidos();
        void CriarPedido(Pedido novoPedido);
       void DeletarPedido(int id);
    }
}