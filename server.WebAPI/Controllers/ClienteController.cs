using Microsoft.AspNetCore.Mvc;
using server.Domain;
using server.Domain.Repositories;
using server.Infra.Data.Repositories;

namespace server.WebAPI.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _respository;

        public ClienteController()
        {
            _respository = new ClienteRepository();
        }

        [HttpPost]
        public IActionResult PostCliente([FromBody] Cliente novoCliente)
        {
            try
            {
                var resultado = _respository.CriarCliente(novoCliente);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPut]
        public IActionResult PutCliente([FromBody] Cliente clienteEditado)
        {
            try
            {
                var resultado = _respository.EditarCliente(clienteEditado);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpDelete("{idCliente}")]
        public IActionResult DeletarCliente( int IdCliente)
        {
            try
            {
                _respository.DeletarCliente(IdCliente);

                return Ok(new Resposta(200, "Deletado com Sucesso"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }


        }

        [HttpGet]
        public IActionResult GetTodosCliente()
        {
            try
            {
                return Ok(_respository.BuscarTodosClientes());
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("ID/{id}")]
        public IActionResult GetClienteId( int id)
        {
            try
            {
                return Ok(_respository.BuscarClienteId(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("CPF")]
        public IActionResult GetClienteCpf([FromBody] string cpf)
        {
            try
            {
                return Ok(_respository.BuscarClienteCpf(cpf));
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