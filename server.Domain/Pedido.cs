using server.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain
{
    public class Pedido
    {
         public int IdPedido { get; set; }
        public Cliente? ClienteId { get; set; }
        public Produto ProdutoId { get; set; }
        public DateTime DataPedido { get; set; }
        public int QuantidadeProduto { get; set; }
        public decimal ValorTotal { get; set; }
        public string StatusPedido { get; set; }


        public Pedido()
        {
            ProdutoId = new Produto();
            ClienteId = new Cliente();
            this.DataPedido = DateTime.Now;
            this.StatusPedido = "Em Andamento";
        }

        public Pedido(int idPedido, Cliente clienteId, Produto produtoId, int quantidadeProduto, string statusPedido, DateTime dataPedido)
        {
            this.IdPedido = idPedido;
            this.ClienteId = clienteId;
            this.ProdutoId = produtoId;
            this.DataPedido = dataPedido;
            this.QuantidadeProduto = quantidadeProduto;
            this.StatusPedido = statusPedido;
        }

        public decimal CalcularValor()
        {
            return QuantidadeProduto*ProdutoId.Valor;
        }

        public void Validar()
        {
            
            if (DataPedido == null)
                throw new PedidoExcepition("data Pedido é obrigatório!");
        }

        

       
    }
}