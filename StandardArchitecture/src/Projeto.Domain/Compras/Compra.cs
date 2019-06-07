using Architecture.Domain.Core.Model;
using FluentValidation;
using Project.Domain.Clientes;
using System;

namespace Project.Domain.Compras
{
    public class Compra : Entity<Compra>
    {
        public Compra
            (string codigo, string mercadoria, int quantidade, decimal valorUnitario, decimal valorTotalMercadoria, decimal freteCompra,
            decimal despesaNF, decimal totalNF, decimal custoUnitario, DateTime dataCompra, string observacao, Guid clienteId)
        {
            Id = Guid.NewGuid();
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

        public string Codigo { get; private set; }
        public string Mercadoria { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal ValorTotalMercadoria { get; private set; }
        public decimal FreteCompra { get; private set; }
        public decimal DespesaNF { get; private set; }
        public decimal TotalNF { get; private set; }
        public decimal CustoUnitario { get; private set; }
        public DateTime DataCompra { get; private set; }
        public string Observacao { get; private set; }
        public bool Excluido { get; private set; }
        public Guid ClienteId { get; private set; }

        // EF propriedades de navegação 
        public virtual Cliente Cliente { get; private set; }

        // Construtor para o EF
        protected Compra() { }

        public void ExcluirCompra()
        {
            Excluido = true;
        }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações
        private void Validar()
        {
            ValidarCodigo();
            ValidarMercadoria();
            ValidarQuantidade();
            ValidarValorUnitario();
            ValidarValorTotalMercadoria();
            ValidarFreteCompra();
            ValidarDespesaNF();
            ValidarTotalNF();
            ValidarCustoUnitario();
            ValidarDataCompra();
            ValidarObservacao();
            ValidationResult = Validate(this);
        }

        private void ValidarCodigo()
        {
            RuleFor(c => c.Codigo)
               .NotEmpty().WithMessage("O código precisa ser fornecido.")
               .Length(1, 7).WithMessage("O código precisa ter entre 1 e 7 caracteres.");
        }

        private void ValidarMercadoria()
        {
            RuleFor(c => c.Mercadoria)
               .NotEmpty().WithMessage("A mercadoria precisa ser fornecido.")
               .Length(2, 100).WithMessage("A mercadoria precisa ter entre 2 e 100 caracteres.");
        }

        private void ValidarQuantidade()
        {
            RuleFor(c => c.Quantidade)
               .NotEmpty().WithMessage("A quantidade precisa ser fornecido.")
               .GreaterThan(0).WithMessage("A quantidade precisa ser maior que 0.");
        }

        private void ValidarValorUnitario()
        {
            RuleFor(c => c.ValorUnitario)
               .NotEmpty().WithMessage("O valor unitário precisa ser fornecido.")
               .GreaterThan(0).WithMessage("O valor unitário precisa ser maior que 0.");
        }

        private void ValidarValorTotalMercadoria()
        {
            RuleFor(c => c.ValorTotalMercadoria)
               .NotEmpty().WithMessage("O valor total da mercadoria precisa ser fornecido.")
               .GreaterThan(0).WithMessage("O valor total da mercadoria precisa ser maior que 0.");
        }

        private void ValidarFreteCompra()
        {
            RuleFor(c => c.FreteCompra)
               .NotEmpty().WithMessage("O frete - compra precisa ser fornecido.")
               .GreaterThan(0).WithMessage("O frete - compra precisa ser maior que 0.");
        }

        private void ValidarDespesaNF()
        {
            RuleFor(c => c.DespesaNF)
               .NotEmpty().WithMessage("A Despesa NF precisa ser fornecido.")
               .GreaterThan(0).WithMessage("A Despesa NF precisa ser maior que 0.");
        }

        private void ValidarTotalNF()
        {
            RuleFor(c => c.TotalNF)
               .NotEmpty().WithMessage("O valor total da NF precisa ser fornecido.")
               .GreaterThan(0).WithMessage("O valor total da NF precisa ser maior que 0.");
        }

        private void ValidarCustoUnitario()
        {
            RuleFor(c => c.CustoUnitario)
               .NotEmpty().WithMessage("O custo unitário precisa ser fornecido.")
               .GreaterThan(0).WithMessage("O custo unitário precisa ser maior que 0.");
        }

        private void ValidarDataCompra()
        {
            RuleFor(c => c.DataCompra)
               .LessThan(DateTime.Now)
               .WithMessage("A data da compra precisa ser menor que a data de hoje.");
        }

        private void ValidarObservacao()
        {
            RuleFor(c => c.Mercadoria)
               .Length(3, 100).WithMessage("A observação precisa ter entre 2 e 100 caracteres");
        }
        #endregion

        public static class CompraFactory
        {
            public static Compra NovoCompraCompleto(
                Guid id,
                string codigo, string mercadoria, int quantidade, decimal valorUnitario, decimal valorTotalMercadoria,
                decimal freteCompra, decimal despesaNF, decimal totalNF, decimal custoUnitario, DateTime dataCompra,
                string observacao, Guid clienteId)
            {
                var compra = new Compra()
                {
                    Id = id,
                    Codigo = codigo,
                    Mercadoria = mercadoria,
                    Quantidade = quantidade,
                    ValorUnitario = valorUnitario,
                    ValorTotalMercadoria = valorTotalMercadoria,
                    FreteCompra = freteCompra,
                    DespesaNF = despesaNF,
                    TotalNF = totalNF,
                    CustoUnitario = custoUnitario,
                    DataCompra = dataCompra,
                    Observacao = observacao,
                    ClienteId = clienteId
                };

                return compra;
            }
        }
    }
}