using AutoMapper;
using Project.Application.ViewModels;
using Project.Domain.Compras.Commands;

namespace Project.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //Compras 
            CreateMap<CompraViewModel, RegistrarCompraCommand>()
                .ConstructUsing(c => new RegistrarCompraCommand(
                                        c.Codigo,
                                        c.Mercadoria,
                                        c.Quantidade,
                                        c.ValorUnitario,
                                        c.ValorTotalMercadoria,
                                        c.FreteCompra,
                                        c.DespesaNF,
                                        c.TotalNF,
                                        c.CustoUnitario,
                                        c.DataCompra,
                                        c.Observacao,
                                        c.ClienteId));

            CreateMap<CompraViewModel, AtualizarCompraCommand>()
                .ConstructUsing(c => new AtualizarCompraCommand(c.Id, c.Codigo,
                                        c.Mercadoria,
                                        c.Quantidade,
                                        c.ValorUnitario,
                                        c.ValorTotalMercadoria,
                                        c.FreteCompra,
                                        c.DespesaNF,
                                        c.TotalNF,
                                        c.CustoUnitario,
                                        c.DataCompra,
                                        c.Observacao,
                                        c.ClienteId));

            CreateMap<CompraViewModel, ExcluirCompraCommand>()
                .ConstructUsing(c => new ExcluirCompraCommand(c.Id));
        }
    }
}
