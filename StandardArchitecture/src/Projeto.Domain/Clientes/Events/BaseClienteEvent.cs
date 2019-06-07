using Architecture.Domain.Core.Events;
using System;

namespace Project.Domain.Clientes.Events
{
    public abstract class BaseClienteEvent : Event
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public string CPF { get; protected set; }
        public string Email { get; protected set; }
    }
}
