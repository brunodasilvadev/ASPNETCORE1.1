using Project.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace Project.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        void Registrar(ClienteViewModel clienteViewModel);
        IEnumerable<ClienteViewModel> ObterTodos();
        ClienteViewModel ObterPorId(Guid id);
        void Atualizar(ClienteViewModel compraViewModel);
        void Excluir(Guid id);
    }
}
