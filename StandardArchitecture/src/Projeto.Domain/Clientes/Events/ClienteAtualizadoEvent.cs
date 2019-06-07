using System;

namespace Project.Domain.Clientes.Events
{
    public class ClienteAtualizadoEvent : BaseClienteEvent
    {
        public ClienteAtualizadoEvent(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
            AggregateId = id;
        }
    }
}
