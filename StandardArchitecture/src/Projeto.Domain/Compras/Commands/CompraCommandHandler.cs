using Architecture.Domain.Core.Bus;
using Architecture.Domain.Core.CommandHandlers;
using Architecture.Domain.Core.Events;
using Architecture.Domain.Core.Interfaces;
using Architecture.Domain.Core.Notifications;
using Project.Domain.Compras.Events;
using Project.Domain.Compras.Repository;
using System;

namespace Project.Domain.Compras.Commands
{
    public class CompraCommandHandler : CommandHandler,
        IHandler<RegistrarCompraCommand>,
        IHandler<AtualizarCompraCommand>,
        IHandler<ExcluirCompraCommand>
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IBus _bus;
        private readonly IUser _user;

        public CompraCommandHandler(ICompraRepository compraRepository, 
                                    IUnitOfWork uow, 
                                    IBus bus,
                                    IDomainNotificationHandler<DomainNotification> notifications, 
                                    IUser user) 
                                    : base(uow, bus, notifications)
        {
            _compraRepository = compraRepository;
            _bus = bus;
            _user = user;
        }

        public void Handle(RegistrarCompraCommand message)
        {
            decimal valorTotalMercadoria = ValorTotalMercadoria(message.Quantidade, message.ValorUnitario);
            decimal totalNF = TotalNF(valorTotalMercadoria, message.FreteCompra, message.DespesaNF);
            decimal custoUnitario = CustoUnitario(totalNF, message.Quantidade);

            var compra = new Compra(message.Codigo, message.Mercadoria, message.Quantidade, message.ValorUnitario,
                valorTotalMercadoria, message.FreteCompra, message.DespesaNF, totalNF, custoUnitario,
                message.DataCompra, message.Observacao, message.ClienteId);

            if (!CompraValido(compra)) return;

             _compraRepository.Adicionar(compra);

            if (Commit())
            {
                _bus.RaiseEvent(new CompraRegistradoEvent(compra.Id, compra.Codigo, compra.Mercadoria, compra.Quantidade,
                    compra.ValorUnitario, compra.ValorTotalMercadoria, compra.FreteCompra, compra.DespesaNF, compra.TotalNF,
                    compra.CustoUnitario, compra.DataCompra, compra.Observacao));
            }
        }

        public void Handle(AtualizarCompraCommand message)
        {
            var compraAtual = _compraRepository.ObterPorId(message.Id);

            if (!CompraExistente(message.Id, message.MessageType)) return;

            if (compraAtual.ClienteId != _user.GetUserId())
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Compra não pertence ao cliente."));
                return;
            }

            decimal valorTotalMercadoria = ValorTotalMercadoria(message.Quantidade, message.ValorUnitario);
            decimal totalNF = TotalNF(valorTotalMercadoria, message.FreteCompra, message.DespesaNF);
            decimal custoUnitario = CustoUnitario(totalNF, message.Quantidade);

            var compra = Compra.CompraFactory.NovoCompraCompleto(message.Id, message.Codigo, message.Mercadoria, message.Quantidade, message.ValorUnitario,
                   valorTotalMercadoria, message.FreteCompra, message.DespesaNF, totalNF, custoUnitario,
                   message.DataCompra, message.Observacao, message.ClienteId);

            if (!CompraValido(compra)) return;

            _compraRepository.Atualizar(compra);

            if (Commit())
            {
                _bus.RaiseEvent(new CompraAtualizadoEvent(compra.Id, compra.Codigo, compra.Mercadoria, compra.Quantidade, compra.ValorUnitario,
                   compra.ValorTotalMercadoria, compra.FreteCompra, compra.DespesaNF, compra.TotalNF, compra.CustoUnitario,
                   compra.DataCompra, compra.Observacao));
            }
        }

        public void Handle(ExcluirCompraCommand message)
        {
            if (!CompraExistente(message.Id, message.MessageType)) return;

            var compraAtual = _compraRepository.ObterPorId(message.Id);

            if (compraAtual.ClienteId != _user.GetUserId())
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Compra não pertence ao cliente."));
                return;
            }

            compraAtual.ExcluirCompra();

            _compraRepository.Atualizar(compraAtual);

            if (Commit())
            {
                _bus.RaiseEvent(new CompraExcluidoEvent(message.Id));
            }
        }

        private bool CompraValido(Compra compra)
        {
            if (compra.EhValido()) return true;

            NotificarValidacoesErro(compra.ValidationResult);
            return false;
        }

        private bool CompraExistente(Guid id, string messageType)
        {
            var compra = _compraRepository.ObterPorId(id);

            if (compra != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Compra não encontrada."));
            return false;
        }

        private decimal ValorTotalMercadoria(int quantidade, decimal valorUnitario)
        {
            return quantidade * valorUnitario;
        }

        private decimal TotalNF(decimal valorTotalMercadoria, decimal freteCompra, decimal despesaNF)
        {
            return valorTotalMercadoria + freteCompra + despesaNF;
        }

        private decimal CustoUnitario(decimal totalNF, int quantidade)
        {
            return totalNF / quantidade;
        }
    }
}
