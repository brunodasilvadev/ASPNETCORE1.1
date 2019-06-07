using System;

namespace Project.Domain.Compras.Commands
{
    public class RegistrarCompraCommand : BaseCompraCommand
    {
        public RegistrarCompraCommand
            (string codigo, string mercadoria, int quantidade, decimal valorUnitario, decimal valorTotalMercadoria,
            decimal freteCompra, decimal despesaNF, decimal totalNF, decimal custoUnitario, DateTime dataCompra,
            string observacao, Guid clienteId)
        {
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
            ClienteId = clienteId;
        }
    }
}
