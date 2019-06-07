using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Application.ViewModels
{
    public class CompraViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O código é requerido")]
        [MinLength(1, ErrorMessage = "O tamanho mínimo do código é {1}")]
        [MaxLength(7, ErrorMessage = "O tamanho máximo do código é {1}")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "A mercadoria é requerida")]
        [MinLength(2, ErrorMessage = "O tamanho mínimo da mercadoria é {1}")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo da mercadoria é {1}")]
        [Display(Name = "Mercadoria")]
        public string Mercadoria { get; set; }

        [Required(ErrorMessage = "A quantidade é requerida")]
        public int Quantidade { get; set; }

        [Display(Name = "Valor unitário")]
        [Required(ErrorMessage = "O valor unitário é requerido")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency, ErrorMessage = "Moeda em formato inválido")]
        public decimal ValorUnitario { get; set; }

        [Display(Name = "Total da Mercadoria")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorTotalMercadoria { get; set; }

        [Display(Name = "Frete - Compra")]
        [Required(ErrorMessage = "O Frete - Compra é requerida")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency, ErrorMessage = "Moeda em formato inválido")]
        public decimal FreteCompra { get; set; }

        [Display(Name = "Despesa - NF")]
        [Required(ErrorMessage = "A Despesa - NF é requerida")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency, ErrorMessage = "Moeda em formato inválido")]
        public decimal DespesaNF { get; set; }

        [Display(Name = "Total da NF")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalNF { get; set; }

        [Display(Name = "Custo Unitário")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal CustoUnitario { get; set; }

        [Display(Name = "Data Compra")]
        public DateTime DataCompra { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public Guid ClienteId { get; set; }
    }
}
