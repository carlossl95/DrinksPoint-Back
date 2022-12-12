using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Repositories
{
    public interface IClienteRepository
    {
        Cliente CriarCliente(Cliente novoCliente);
        Cliente EditarCliente(Cliente clienteEditado);

        void DeletarCliente(int idCliente);

        List<Cliente> BuscarTodosClientes();
        Cliente BuscarClienteId(int id);
        Cliente BuscarClienteCpf(string cpf);

    }
}