using System;

namespace Project.Domain.Clientes.Commands
{
    public class RegistrarClienteCommand : BaseClienteCommand
    {
        public RegistrarClienteCommand(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }
    }
}
