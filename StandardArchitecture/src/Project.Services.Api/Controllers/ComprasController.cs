using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Architecture.Domain.Core.Bus;
using Architecture.Domain.Core.Interfaces;
using Architecture.Domain.Core.Notifications;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Interfaces;
using Project.Application.ViewModels;
using Project.Domain.Compras.Commands;
using Project.Domain.Compras.Repository;

namespace Project.Services.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Compras")]
    public class ComprasController : BaseController
    {
        private readonly ICompraAppService _compraAppService;
        private readonly IBus _bus;
        private readonly ICompraRepository _compraRepository;
        private readonly IMapper _mapper;

        public ComprasController(IDomainNotificationHandler<DomainNotification> notifications, 
                                IUser user,
                                ICompraAppService compraAppService,
                                IBus bus,
                                ICompraRepository compraRepository,
                                IMapper mapper) : base(notifications, user, bus)
        {
            _compraAppService = compraAppService;
            _compraRepository = compraRepository;
            _mapper = mapper;
            _bus = bus;
        }

        [HttpGet]
        [Route("compras")]
        [AllowAnonymous]
        public IEnumerable<CompraViewModel> Get()
        {
            return _compraAppService.ObterTodos();
        }

        [HttpGet]
        [Route("compras/{id:guid}")]
        [AllowAnonymous]
        public CompraViewModel Get(Guid id, int version)
        {
            return _compraAppService.ObterPorId(id);
        }

        [HttpPost]
        [Route("compras")]
        [Authorize(Policy = "PodeGravarCompras")]
        public IActionResult Post([FromBody]CompraViewModel compraViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response();
            }

            var compraCommand = _mapper.Map<RegistrarCompraCommand>(compraViewModel);

            _bus.SendCommand(compraCommand);
            return Response(compraCommand);
        }

        [HttpPut]
        [Route("compras")]
        [Authorize(Policy = "PodeGravarCompras")]
        public IActionResult Put([FromBody]CompraViewModel compraViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response();
            }

            _compraAppService.Atualizar(compraViewModel);
            return Response(compraViewModel);
        }

        [HttpDelete]
        [Route("eventos/{id:guid}")]
        [Authorize(Policy = "PodeGravar")]
        public IActionResult Delete(Guid id)
        {
            _compraAppService.Excluir(id);
            return Response();
        }
    }
}