using Architecture.Domain.Core.Bus;
using Architecture.Domain.Core.CommandHandlers;
using Architecture.Domain.Core.Events;
using Architecture.Domain.Core.Interfaces;
using Architecture.Domain.Core.Notifications;
using Project.Domain.Clientes.Events;
using Project.Domain.Clientes.Repository;
using System;
using System.Linq;

namespace Project.Domain.Clientes.Commands
{
    public class ClienteCommandHandler : CommandHandler, 
        IHandler<RegistrarClienteCommand>, 
        IHandler<AtualizarClienteCommand>, 
        IHandler<ExcluirClienteCommand>
    {
        private readonly IBus _bus;
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IUnitOfWork uow,
                                    IBus bus,
                                    IDomainNotificationHandler<DomainNotification> notifications,
                                    IClienteRepository clienteRepository)
                                    : base(uow, bus, notifications)
        {
            _bus = bus;
            _clienteRepository = clienteRepository;
        }

        public void Handle(RegistrarClienteCommand message)
        {
            var cliente = new Cliente(message.Id, message.Nome, message.CPF, message.Email);

            if (!cliente.EhValido())
            {
                NotificarValidacoesErro(cliente.ValidationResult);
                return;
            }

            var clienteExistente = _clienteRepository.Buscar(c => c.CPF == cliente.CPF || c.Email == cliente.Email);

            if (clienteExistente.Any())
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "CPF ou e-mail já utilizados."));
            }

            _clienteRepository.Adicionar(cliente);

            if (Commit())
            {
                _bus.RaiseEvent(new ClienteRegistradoEvent(cliente.Id, cliente.Nome, cliente.CPF, cliente.Email));
            }
        }

        public void Handle(AtualizarClienteCommand message)
        {
            if (!ClienteExistente(message.Id, message.MessageType)) return;

            var cliente = new Cliente(message.Id, message.Nome, message.CPF, message.Email);

            if (!ClienteValido(cliente)) return;

            _clienteRepository.Atualizar(cliente);

            if (Commit())
            {
                _bus.RaiseEvent(new ClienteAtualizadoEvent(cliente.Id, cliente.Nome, cliente.CPF, cliente.Email));
            }

        }

        public void Handle(ExcluirClienteCommand message)
        {
            if (!ClienteExistente(message.Id, message.MessageType)) return;

            _clienteRepository.Remover(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new ClienteExcluidoEvent(message.Id));
            }

        }

        private bool ClienteValido(Cliente cliente)
        {
            if (cliente.EhValido()) return true;

            NotificarValidacoesErro(cliente.ValidationResult);
            return false;
        }

        private bool ClienteExistente(Guid id, string messageType)
        {
            var cliente = _clienteRepository.ObterPorId(id);

            if (cliente != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Cliente não encontrado."));
            return false;
        }

    }
}
