using System;

namespace Project.Domain.Compras.Commands
{
    public class ExcluirCompraCommand : BaseCompraCommand
    {
        public ExcluirCompraCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }
    }
}
