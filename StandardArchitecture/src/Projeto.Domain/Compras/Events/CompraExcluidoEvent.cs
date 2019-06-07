using System;

namespace Project.Domain.Compras.Events
{
    public class CompraExcluidoEvent : BaseCompraEvent
    {
        public CompraExcluidoEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
