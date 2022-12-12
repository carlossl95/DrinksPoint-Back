using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Exceptions
{
    public class PedidoExcepition : Exception
    {
        public PedidoExcepition(string mensagem) : base(mensagem)
        {
            
        }
    }
}