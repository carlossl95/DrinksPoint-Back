using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Exceptions
{
    public class ProdutoException : Exception
    {
        public ProdutoException(string mensagem) : base(mensagem)
        {
            
        }
    }
}