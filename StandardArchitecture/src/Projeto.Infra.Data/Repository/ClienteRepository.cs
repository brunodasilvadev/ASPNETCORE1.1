using Dapper;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Clientes;
using Project.Domain.Clientes.Repository;
using Project.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Infra.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ProjectContext context) : base(context)
        {
        }

        public override IEnumerable<Cliente> ObterTodos()
        {
            var sql = @"SELECT * FROM CLIENTES C " +
                "WHERE C.EXCLUIDO = 0 " +
                "ORDER BY C.NOME ";

            return Db.Database.GetDbConnection().Query<Cliente>(sql);
        }

        public override Cliente ObterPorId(Guid id)
        {
            var sql = @"SELECT * FROM CLIENTES C " +
                "WHERE C.ID = @oid ";

            return Db.Database.GetDbConnection().Query<Cliente>(sql, new { oid = id }).FirstOrDefault();
        }
    }
}
