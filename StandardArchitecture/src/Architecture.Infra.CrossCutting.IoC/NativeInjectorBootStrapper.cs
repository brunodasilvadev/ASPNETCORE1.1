using Architecture.Domain.Core.Bus;
using Architecture.Domain.Core.Events;
using Architecture.Domain.Core.Interfaces;
using Architecture.Domain.Core.Notifications;
using Architecture.Infra.CrossCutting.AspNetFilters;
using Architecture.Infra.CrossCutting.Bus;
using Architecture.Infra.CrossCutting.Identity.Models;
using Architecture.Infra.CrossCutting.Identity.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project.Application.Interfaces;
using Project.Application.Services;
using Project.Domain.Clientes.Commands;
using Project.Domain.Clientes.Events;
using Project.Domain.Clientes.Repository;
using Project.Domain.Compras.Commands;
using Project.Domain.Compras.Events;
using Project.Domain.Compras.Repository;
using Project.Infra.Data.Context;
using Project.Infra.Data.Repository;
using Project.Infra.Data.UoW;

namespace Architecture.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<ICompraAppService, CompraAppService>();
            services.AddScoped<IClienteAppService, ClienteAppService>();

            // Domain - Commands
            services.AddScoped<IHandler<RegistrarCompraCommand>, CompraCommandHandler>();
            services.AddScoped<IHandler<AtualizarCompraCommand>, CompraCommandHandler>();
            services.AddScoped<IHandler<ExcluirCompraCommand>, CompraCommandHandler>();
            services.AddScoped<IHandler<RegistrarClienteCommand>, ClienteCommandHandler>();
            services.AddScoped<IHandler<AtualizarClienteCommand>, ClienteCommandHandler>();
            services.AddScoped<IHandler<ExcluirClienteCommand>, ClienteCommandHandler>();

            // Domain - Eventos
            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IHandler<CompraRegistradoEvent>, CompraEventHandler>();
            services.AddScoped<IHandler<CompraAtualizadoEvent>, CompraEventHandler>();
            services.AddScoped<IHandler<CompraExcluidoEvent>, CompraEventHandler>();
            services.AddScoped<IHandler<ClienteRegistradoEvent>, ClienteEventHandler>();
            services.AddScoped<IHandler<ClienteAtualizadoEvent>, ClienteEventHandler>();
            services.AddScoped<IHandler<ClienteExcluidoEvent>, ClienteEventHandler>();

            // Infra - Data
            services.AddScoped<ICompraRepository, CompraRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ProjectContext>();

            // Infra - Bus
            services.AddScoped<IBus, InMemoryBus>();

            // Infra - Filtros
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();

            // Infra - Identity 
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IUser, AspNetUser>();

        }
    }
}
