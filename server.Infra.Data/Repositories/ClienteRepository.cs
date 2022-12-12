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
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDao _clienteDao;
        private readonly PedidoDao _pedidoDao;
        private readonly Cliente _cliente;


        public ClienteRepository()
        {
            _clienteDao = new ClienteDao();
            _pedidoDao= new PedidoDao();
            _cliente = new Cliente();
        }

        public Cliente BuscarClienteCpf(string cpf)
        {
             var clienteBuscado = _clienteDao.BuscarPorCpf(cpf);
            if (clienteBuscado == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            return clienteBuscado;
        }

        public Cliente BuscarClienteId(int id)
        {
            var clienteBuscado = _clienteDao.BuscarPorId(id);
            if (clienteBuscado == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            return clienteBuscado;
        }

        public List<Cliente> BuscarTodosClientes()
        {
           var listaClientes = _clienteDao.BuscarTodosClientes();
           if (listaClientes.Count == 0)
           {
                throw new Exception("Não existe nenhum registro!");
           }
           return listaClientes;
        }

        public Cliente CriarCliente(Cliente novoCliente)
        {
            novoCliente.Validar();

            var clienteBuscado = _clienteDao.BuscarPorCpf(novoCliente.Cpf);
            if (clienteBuscado != null)
                throw new ClienteException("Um cadastro com esse CPF já existe!");

            _clienteDao.Adicionar(novoCliente);

            var clienteAdicionado = _clienteDao.BuscarPorCpf(novoCliente.Cpf);
            if (clienteAdicionado == null)
                throw new ClienteException("Não foi possível adicionar um novo cliente!");

            return novoCliente;
        }

        public void DeletarCliente(int idCliente)
        {
            
            var clienteBuscado = _clienteDao.BuscarPorId(idCliente);
            if (clienteBuscado == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            else
            {
                _pedidoDao.DeletarPedidoIdCliente(idCliente);
                _clienteDao.DeletarCliente(idCliente);
            }
        }

        public Cliente EditarCliente(Cliente clienteEditado)
        {
            var clienteBuscado = _clienteDao.BuscarPorId(clienteEditado.IdCliente);
            if (clienteBuscado == null)
                throw new ClienteException("Não existe este Id no Bando de Dados");
            else
            {
                _clienteDao.EditarCliente(clienteEditado);
                return clienteEditado;
            }
            

        }
    }
}