using Architecture.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Project.Domain.Compras.Repository
{
    public interface ICompraRepository : IRepository<Compra>
    {
        IEnumerable<Compra> ObterCompraPorCliente(Guid clienteId);
    }

}
