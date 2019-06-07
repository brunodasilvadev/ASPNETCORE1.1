using Architecture.Domain.Core.Commands;
using System;

namespace Project.Domain.Compras.Commands
{
    public abstract class BaseCompraCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Codigo { get; protected set; }
        public string Mercadoria { get; protected set; }
        public int Quantidade { get; protected set; }
        public decimal ValorUnitario { get; protected set; }
        public decimal ValorTotalMercadoria { get; protected set; }
        public decimal FreteCompra { get; protected set; }
        public decimal DespesaNF { get; protected set; }
        public decimal TotalNF { get; protected set; }
        public decimal CustoUnitario { get; protected set; }
        public DateTime DataCompra { get; protected set; }
        public string Observacao { get; protected set; }
        public Guid ClienteId { get; protected set; }
    }

}
