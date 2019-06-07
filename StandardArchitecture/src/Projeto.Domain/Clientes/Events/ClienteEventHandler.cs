using Architecture.Domain.Core.Events;
using System;

namespace Project.Domain.Clientes.Events
{
    public class ClienteEventHandler :
        IHandler<ClienteRegistradoEvent>,
        IHandler<ClienteAtualizadoEvent>,
        IHandler<ClienteExcluidoEvent>
    {
        public void Handle(ClienteRegistradoEvent message)
        {
            Console.WriteLine("Cliente Registrado com sucesso");
        }

        public void Handle(ClienteAtualizadoEvent message)
        {
            Console.WriteLine("Cliente Atualizado com sucesso");
        }

        public void Handle(ClienteExcluidoEvent message)
        {
            Console.WriteLine("Cliente Excluido com sucesso");
        }
    }
}
