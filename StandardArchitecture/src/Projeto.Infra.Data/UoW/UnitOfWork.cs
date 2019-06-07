using Architecture.Domain.Core.Commands;
using Architecture.Domain.Core.Interfaces;
using Project.Infra.Data.Context;

namespace Project.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectContext _context;

        public UnitOfWork(ProjectContext context)
        {
            _context = context;
        }

        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
