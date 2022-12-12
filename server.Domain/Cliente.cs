using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Exceptions;

namespace server.Domain
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string? Cpf { get; set; }
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Pontos { get; set; }
        public Cliente()
        {
            if (this.Pontos == null)
            {
                this.Pontos = 0;
            }            
        }

        public Cliente(string cpf, string nome, DateTime dataNascimento, decimal pontos)
        { 
            this.Cpf = cpf;
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Pontos = pontos;            
        }
        

        public void Validar()
        {
            if (Cpf.Length < 11 )
                throw new ClienteException("CPF deve conter 11 digitos");
            if (Cpf.All(char.IsDigit) == false)
                throw new ClienteException("CPF deve conter apenas digitos numericos");
            if (Nome.Length < 3)
                throw new ClienteException("Nome deve conter no minimo 3 Caracteres");
            if (DataNascimento == null)
                throw new ClienteException("data nascimento é obrigatório!");
            if (DataNascimento >= DateTime.Now)
                throw new ClienteException("Data nascimento deve ser anterior de hoje!");
        }
    }
}