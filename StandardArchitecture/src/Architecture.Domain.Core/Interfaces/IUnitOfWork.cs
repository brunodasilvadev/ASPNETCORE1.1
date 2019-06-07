using Architecture.Domain.Core.Commands;
using System;

namespace Architecture.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}
