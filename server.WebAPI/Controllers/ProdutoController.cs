using Microsoft.AspNetCore.Mvc;
using server.Domain;
using server.Domain.Repositories;
using server.Infra.Data.Repositories;

namespace server.WebAPI.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repository;

        public ProdutoController()
        {
            _repository = new ProdutoRepository();
        }

        [HttpPost]
        public IActionResult PostProduto([FromBody] Produto novoProduto)
        {
            try
            {
                _repository.AdicionarProduto(novoProduto);
                return Ok(new Resposta(200, "Criado com sucesso!"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet]
        public IActionResult GetProdutos()
        {
            try
            {
                return Ok(_repository.BuscarTodosProdutos());
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet]
        [Route("ID/{id}")]
        public IActionResult GetProdutosId(int id)
        {
            try
            {
                return Ok(_repository.BuscarProdutoId(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("ativo")]
        public IActionResult GetProdutosAtivo()
        {
            try
            {
                return Ok(_repository.BuscarTodosProdutosAtivos());
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPut]
        public IActionResult PutProduto([FromBody] Produto produtoEditado)
        {
            try
            {
                return Ok(_repository.EditarProduto(produtoEditado));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPut("estoque")]
        public IActionResult PutEstoqueProduto([FromBody] Produto produtoEditado)
        {
            try
            {
                _repository.AtualizaEstoque(produtoEditado);
                return Ok(new Resposta(200, "Estoque Atualizado"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpDelete("{idProduto}")]
        public IActionResult DeleteProduto( int idProduto)
        {
            try
            {
                _repository.DeletarProdutoId(idProduto);
                return Ok(new Resposta(200, "Deletado com Sucesso"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

        }

        [HttpPut]
        [Route("status")]
        public IActionResult AtivaDesativaProduto(Produto produtoEditado)
        {
            try
            {
                _repository.AtivaDesativaProduto(produtoEditado);

                return Ok(new Resposta(200, "Status Produto alterado com sucesso"));
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