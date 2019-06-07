using System;

namespace Project.Domain.Clientes.Events
{
    public class ClienteRegistradoEvent : BaseClienteEvent
    {
        public ClienteRegistradoEvent(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }
    }
}
