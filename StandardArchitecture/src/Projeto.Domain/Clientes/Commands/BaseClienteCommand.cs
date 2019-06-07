using Architecture.Domain.Core.Commands;
using System;

namespace Project.Domain.Clientes.Commands
{
    public abstract class BaseClienteCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public string CPF { get; protected set; }
        public string Email { get; protected set; }
    }
}
