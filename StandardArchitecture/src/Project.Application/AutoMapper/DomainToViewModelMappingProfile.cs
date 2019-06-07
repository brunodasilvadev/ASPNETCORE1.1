using AutoMapper;
using Project.Application.ViewModels;
using Project.Domain.Clientes;
using Project.Domain.Compras;

namespace Project.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Compra, CompraViewModel>();
            CreateMap<Cliente, ClienteViewModel>();
        }
    }
}
