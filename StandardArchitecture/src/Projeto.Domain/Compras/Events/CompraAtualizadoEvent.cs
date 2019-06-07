using System;

namespace Project.Domain.Compras.Events
{
    public class CompraAtualizadoEvent : BaseCompraEvent
    {
        public CompraAtualizadoEvent
            (Guid id, string codigo, string mercadoria, int quantidade, decimal valorUnitario,
            decimal valorTotalMercadoria, decimal freteCompra, decimal despesaNF, decimal totalNF,
            decimal custoUnitario, DateTime dataCompra, string observacao)
        {
            Id = id;
            Codigo = codigo;
            Mercadoria = mercadoria;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ValorTotalMercadoria = valorTotalMercadoria;
            FreteCompra = freteCompra;
            DespesaNF = despesaNF;
            TotalNF = totalNF;
            CustoUnitario = custoUnitario;
            DataCompra = dataCompra;
            Observacao = observacao;
            AggregateId = id;
        }
    }
}
