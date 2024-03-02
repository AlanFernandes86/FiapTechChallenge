using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Application.Order.GetOrdersByStatus;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Ports.Services;
using TechChallenge.Domain.Repositories;
using TechChallenge.Infra.Context;
using TechChallenge.Infra.Provider;
using TechChallenge.Infra.Repositories;

namespace TechChallenge.Infra;

public static class DependencyInjection
{
    public static void AddPortsAndAdapters(this IServiceCollection services)
    {
        services.AddServices();
        services.AddRepositories();
        services.AddUseCases();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IProductService, ProductService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IDatabaseProvider, SqlServerProvider>();

        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
    }

    public static void AddUseCases(this IServiceCollection services)
    {
        services.AddTransient<IUseCase<GetOrdersByStatusDAO, UseCaseOutput<IEnumerable<Domain.Entities.Order>>>, GetOrdersByStatusUseCase>();
    }
}