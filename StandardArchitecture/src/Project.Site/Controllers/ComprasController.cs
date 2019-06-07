using Architecture.Domain.Core.Interfaces;
using Architecture.Domain.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Interfaces;
using Project.Application.ViewModels;
using System;

namespace Project.Site.Controllers
{
    public class ComprasController : BaseController
    {
        private readonly ICompraAppService _compraAppService;

        public ComprasController(ICompraAppService compraAppService,
                     IDomainNotificationHandler<DomainNotification> notifications,
                     IUser user) : base(notifications, user)
        {
            _compraAppService = compraAppService;
        }

        [Route("")]
        [Route("minhas-compras")]
        [Authorize(Policy = "PodeLerCompras")]
        public IActionResult Index()
        {
            return View(_compraAppService.ObterCompraPorCliente(ClienteId));
        }

        [Route("dados-da-compra/{id:guid}")]
        [Authorize(Policy = "PodeLerCompras")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compraViewModel = _compraAppService.ObterPorId(id.Value);

            if (compraViewModel == null)
            {
                return NotFound();
            }

            return View(compraViewModel);
        }

        [Route("nova-compra")]
        [Authorize(Policy = "PodeGravarCompras")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("nova-compra")]
        [Authorize(Policy = "PodeGravarCompras")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompraViewModel compraViewModel)
        {
            if (!ModelState.IsValid) return View(compraViewModel);

            compraViewModel.ClienteId = ClienteId;

            _compraAppService.Registrar(compraViewModel);

            ViewBag.RetornoPost = OperacaoValida() ? "success,Compra registrada com sucesso!" : "error,Compra não registrada! verifique as mensagens!";

            return View(compraViewModel);
        }

        [Route("editar-compra/{id:guid}")]
        [Authorize(Policy = "PodeGravarCompras")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compraViewModel = _compraAppService.ObterPorId(id.Value);

            if (compraViewModel == null)
            {
                return NotFound();
            }

            if (ValidarAutoridadeCompra(compraViewModel))
            {
                return RedirectToAction("Index", _compraAppService.ObterCompraPorCliente(ClienteId));
            }

            return View(compraViewModel);
        }

        [Route("editar-compra/{id:guid}")]
        [Authorize(Policy = "PodeGravarCompras")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CompraViewModel compraViewModel)
        {
            if (ValidarAutoridadeCompra(compraViewModel))
            {
                return RedirectToAction("Index", _compraAppService.ObterCompraPorCliente(ClienteId));
            }

            if (!ModelState.IsValid) return View(compraViewModel);

            compraViewModel.ClienteId = ClienteId;

            _compraAppService.Atualizar(compraViewModel);

            ViewBag.RetornoPost = OperacaoValida() ? "success,Compra atualizada com sucesso!" : "error,Compra não pode ser atualizada! verifique as mensagens!";

            compraViewModel = _compraAppService.ObterPorId(compraViewModel.Id);


            return View(compraViewModel);
        }

        [Route("excluir-compra/{id:guid}")]
        [Authorize(Policy = "PodeGravarCompras")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compraViewModel = _compraAppService.ObterPorId(id.Value);

            if (compraViewModel == null)
            {
                return NotFound();
            }

            if (ValidarAutoridadeCompra(compraViewModel))
            {
                return RedirectToAction("Index", _compraAppService.ObterCompraPorCliente(ClienteId));
            }

            return View(compraViewModel);
        }

        [Route("excluir-compra/{id:guid}")]
        [Authorize(Policy = "PodeGravarCompras")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (ValidarAutoridadeCompra(_compraAppService.ObterPorId(id)))
            {
                return RedirectToAction("Index", _compraAppService.ObterCompraPorCliente(ClienteId));
            }

            _compraAppService.Excluir(id);

            return RedirectToAction("Index");
        }

        private bool ValidarAutoridadeCompra(CompraViewModel compraViewModel)
        {
            return compraViewModel.ClienteId != ClienteId;
        }
    }
}
