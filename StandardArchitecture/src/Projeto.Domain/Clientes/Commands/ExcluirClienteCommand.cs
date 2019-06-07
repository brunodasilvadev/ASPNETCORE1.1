using System;

namespace Project.Domain.Clientes.Commands
{
    public class ExcluirClienteCommand : BaseClienteCommand
    {
        public ExcluirClienteCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
