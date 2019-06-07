using Dapper;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Compras;
using Project.Domain.Compras.Repository;
using Project.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Infra.Data.Repository
{
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        public CompraRepository(ProjectContext context) : base(context)
        {
        }

        public override IEnumerable<Compra> ObterTodos()
        {
            var sql = @"SELECT * FROM COMPRAS C " +
                "WHERE C.EXCLUIDO = 0 " +
                "ORDER BY C.DATACOMPRA DESC ";

            return Db.Database.GetDbConnection().Query<Compra>(sql);
        }

        public IEnumerable<Compra> ObterCompraPorCliente(Guid clienteId)
        {
            var sql = @"SELECT * FROM COMPRAS C " +
                "WHERE C.EXCLUIDO = 0 " +
                "AND C.CLIENTEID = @oid " +
                "ORDER BY C.DATACOMPRA DESC";

            return Db.Database.GetDbConnection().Query<Compra>(sql, new { oid = clienteId });
        }

        public override Compra ObterPorId(Guid id)
        {
            var sql = @"SELECT * FROM COMPRAS C " +
                       "WHERE C.ID = @oid ";

            return Db.Database.GetDbConnection().Query<Compra>(sql, new { oid = id }).FirstOrDefault();
        }
    }
}
