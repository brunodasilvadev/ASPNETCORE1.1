using Project.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace Project.Application.Interfaces
{
    public interface ICompraAppService : IDisposable
    {
        void Registrar(CompraViewModel compraViewModel);
        IEnumerable<CompraViewModel> ObterTodos();
        IEnumerable<CompraViewModel> ObterCompraPorCliente(Guid clienteId);
        CompraViewModel ObterPorId(Guid id);
        void Atualizar(CompraViewModel compraViewModel);
        void Excluir(Guid id);
    }
}
