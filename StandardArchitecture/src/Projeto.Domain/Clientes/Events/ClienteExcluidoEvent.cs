using System;

namespace Project.Domain.Clientes.Events
{
    public class ClienteExcluidoEvent : BaseClienteEvent
    {
        public ClienteExcluidoEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
