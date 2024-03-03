using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Application.Order.CreateOrder;
using TechChallenge.Application.Order.GetClient;
using TechChallenge.Application.Order.GetOrdersById;
using TechChallenge.Application.Order.GetOrdersByStatus;
using TechChallenge.Application.Order.GetProductCategories;
using TechChallenge.Application.Order.GetProductsByCategory;
using TechChallenge.Application.Order.PutClient;
using TechChallenge.Application.Order.PutProduct;
using TechChallenge.Application.Order.PutProductCategory;
using TechChallenge.Application.Order.PutProductToOrder;
using TechChallenge.Application.Order.RemoveProductToOrder;
using TechChallenge.Application.Order.SetPayment;
using TechChallenge.Application.Order.UpdateOrderStatus;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Entities;
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
        services.AddTransient<IUseCase<GetClientDAO, UseCaseOutput<Client>>, GetClientUseCase>();
        services.AddTransient<IUseCase<PutClientDAO, UseCaseOutput<bool>>,  PutClientUseCase>();

        services.AddTransient<IUseCase<GetOrdersByStatusDAO, UseCaseOutput<IEnumerable<Order>>>, GetOrdersByStatusUseCase>();
        services.AddTransient<IUseCase<GetOrderByIdDAO, UseCaseOutput<Order>>, GetOrderByIdUseCase>();
        services.AddTransient<IUseCase<UpdateOrderStatusDAO, UseCaseOutput<int>>, UpdateOrderStatusUseCase>();
        services.AddTransient<IUseCase<CreateOrderDAO, UseCaseOutput<int>>, CreateOrderUseCase>();
        services.AddTransient<IUseCase<PutProductToOrderDAO, UseCaseOutput<int>>, PutProductToOrderUseCase>();
        services.AddTransient<IUseCase<RemoveProductToOrderDAO, UseCaseOutput<int>>, RemoveProductToOrderUseCase>();

        services.AddTransient<IUseCase<SetPaymentDAO, UseCaseOutput<int>>, SetPaymentUseCase>();

        services.AddTransient<IUseCase<GetProductCategoriesDAO, UseCaseOutput<IEnumerable<ProductCategory>>>,  GetProductCategoriesUseCase>();
        services.AddTransient<IUseCase<GetProductsByCategoryDAO, UseCaseOutput<IEnumerable<Product>>>,  GetProductsByCategoryUseCase>();
        services.AddTransient<IUseCase<PutProductDAO, UseCaseOutput<int>>, PutProductUseCase>();
        services.AddTransient<IUseCase<PutProductCategoryDAO, UseCaseOutput<int>>, PutProductCategoryUseCase>();
    }

}