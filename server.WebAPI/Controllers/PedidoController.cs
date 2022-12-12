using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Domain;
using server.Domain.Repositories;
using server.Infra.Data.Repositories;

namespace server.WebAPI.Controllers
{

    [ApiController]
    [Route("api/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _respository;
        private readonly IClienteRepository _respositoryCliente;

        public PedidoController()
        {
            _respository = new PedidoRepository();
            _respositoryCliente = new ClienteRepository();
        }

        [HttpGet]
        public IActionResult GetPedido()
        {            
            try
            {
                return Ok(_respository.BuscarTodosPedidos());
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet]
        [Route("ID/{id}")]
        public IActionResult GetClienteId(int id)
        {
            try
            {
                return Ok(_respository.BuscarPedidoId(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPost]
        public IActionResult PostPedido([FromBody] Pedido novoPedido)
        {
            try
            {
                _respository.CriarPedido(novoPedido);
                return Ok(new Resposta(200, "Pedido Realizado"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpDelete("{idPedido}")]
        public IActionResult DeletePedido( int idPedido)
        {
            try
            {
                _respository.DeletarPedido(idPedido);

                return Ok(new Resposta(200, "Deletado com Sucesso"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPut]
        public IActionResult AlteraStatusPedido([FromBody] Pedido pedidoEditado)
        {
            try
            {
                _respository.AlteraStatusPedido(pedidoEditado);

                return Ok(new Resposta(200, "Status Alterado com Sucesso"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }
        

        public class Resposta
        {
            public Resposta(int status, string mensagem)
            {
                this.Status = status;
                this.Mensagem = mensagem;
            }
            public int Status { get; set; }
            public string Mensagem { get; set; }
        }

    }
}