using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Exceptions
{
    public class ClienteException : Exception
    {
        public ClienteException(string mensagem) : base(mensagem)
        {
            
        }
    }
}