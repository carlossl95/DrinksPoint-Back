using server.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace server.Domain
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string? Descricao { get; set; }
        public int? Quantidade { get; set; }
        public decimal Valor { get; set; }
        public DateTime Validade { get; set; }
        public Boolean? Ativo { get; set; }


        public Produto()
        {
            
        }

        public Produto(int idProdutos, string descricao, int quantidade, decimal valor, DateTime validade, Boolean ativo)
        {
            this.IdProduto = idProdutos;
            this.Descricao = descricao;
            this.Quantidade = quantidade;
            this.Valor = valor;
            this.Validade = validade;
            this.Ativo = ativo;
        }

        public void Validar()
        {

            if (Descricao.Length < 3)
                throw new ProdutoException("A descricção deve conter no minimo 3 caracteres");
            if (Valor <= 0)
                throw new ProdutoException("O valor deve ser maior que 0");
            if (Validade <= DateTime.Now)
                throw new ProdutoException("A data de validade deve ser maior que a data de hoje");
        }
    }
}