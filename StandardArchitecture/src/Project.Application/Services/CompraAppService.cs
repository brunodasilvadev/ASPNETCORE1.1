using Architecture.Domain.Core.Bus;
using AutoMapper;
using Project.Application.Interfaces;
using Project.Application.ViewModels;
using Project.Domain.Compras.Commands;
using Project.Domain.Compras.Repository;
using System;
using System.Collections.Generic;

namespace Project.Application.Services
{
    public class CompraAppService : ICompraAppService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly ICompraRepository _compraRepository;

        public CompraAppService(IBus bus, IMapper mapper, ICompraRepository compraRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _compraRepository = compraRepository;
        }

        public void Registrar(CompraViewModel compraViewModel)
        {
            var registroCommand = _mapper.Map<RegistrarCompraCommand>(compraViewModel);
            _bus.SendCommand(registroCommand);
        }

        public IEnumerable<CompraViewModel> ObterCompraPorCliente(Guid clienteId)
        {
            return _mapper.Map<IEnumerable<CompraViewModel>>(_compraRepository.ObterCompraPorCliente(clienteId));
        }

        public CompraViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<CompraViewModel>(_compraRepository.ObterPorId(id));
        }

        public IEnumerable<CompraViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<CompraViewModel>>(_compraRepository.ObterTodos());
        }

        public void Atualizar(CompraViewModel compraViewModel)
        {
            var atualizarCompraCommand = _mapper.Map<AtualizarCompraCommand>(compraViewModel);
            _bus.SendCommand(atualizarCompraCommand);
        }

        public void Excluir(Guid id)
        {
            _bus.SendCommand(new ExcluirCompraCommand(id));
        }

        public void Dispose()
        {
            _compraRepository.Dispose();
        }
    }
}
