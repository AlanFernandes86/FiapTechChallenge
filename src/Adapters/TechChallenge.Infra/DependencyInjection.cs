using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Ports.Repositories;
using TechChallenge.Domain.Ports.Services;
using TechChallenge.Infra.Repositories;

namespace TechChallenge.Infra
{
    public static class DependencyInjection
    {
        public static void AddPortsAndAdapters(this IServiceCollection services)
        {
            services.AddServices();
            services.AddRepositories();
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
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}