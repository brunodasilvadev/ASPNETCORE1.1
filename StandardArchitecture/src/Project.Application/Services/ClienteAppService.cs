using Architecture.Domain.Core.Bus;
using AutoMapper;
using Project.Application.Interfaces;
using Project.Application.ViewModels;
using Project.Domain.Clientes.Commands;
using Project.Domain.Clientes.Repository;
using System;
using System.Collections.Generic;

namespace Project.Application.Services
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly IBus _bus;

        public ClienteAppService(IMapper mapper, IClienteRepository clienteRepository, IBus bus)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _bus = bus;
        }

        public void Registrar(ClienteViewModel clienteViewModel)
        {
            var registrarCommand = _mapper.Map<RegistrarClienteCommand>(clienteViewModel);
            _bus.SendCommand(registrarCommand);
        }

        public IEnumerable<ClienteViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(_clienteRepository.ObterTodos());
        }

        public ClienteViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorId(id));
        }

        public void Atualizar(ClienteViewModel clienteViewModel)
        {
            var atualizarClienteCommand = _mapper.Map<AtualizarClienteCommand>(clienteViewModel);
            _bus.SendCommand(atualizarClienteCommand);
        }

        public void Excluir(Guid id)
        {
            _bus.SendCommand(new ExcluirClienteCommand(id));
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
        }
    }
}
