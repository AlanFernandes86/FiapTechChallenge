using FluentValidation;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.CreateOrder;

public class CreateOrderUseCase : IUseCase<CreateOrderDAO, UseCaseOutput<int>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<CreateOrderDAO> _validator;

    public CreateOrderUseCase(IOrderRepository orderRepository, IValidator<CreateOrderDAO> validator)
    {
        _orderRepository = orderRepository;
        _validator = validator;
    }

    public async Task<UseCaseOutput<int>> Handle(CreateOrderDAO input)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(input);

            if (!validationResult.IsValid)
            {
                return new UseCaseOutput<int>(new Validation(validationResult.Errors.FirstOrDefault()?.ErrorCode!, validationResult.Errors.FirstOrDefault()?.ErrorMessage!));
            }

            var order = new Domain.Entities.Order
            {
                StatusId = OrderStatus.CREATED,
                ClientCpf = string.IsNullOrWhiteSpace(input.ClientCpf) ? "0" : input.ClientCpf,
                ClientName = input.ClientName,
                ProductsOnOrder = input.Products.Select(x => new ProductOnOrder
                {
                    ProductId = x.ProductId,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList()
            };

            var ordersByStatus = await _orderRepository.CreateOrder(order);

            return new UseCaseOutput<int>(ordersByStatus);
        }
        catch (Exception ex)
        {
            var cpfOrAnonymous = input.ClientCpf == string.Empty ? "Anônimo" : input.ClientCpf.ToString();
            return new UseCaseOutput<int>($"Erro ao criar novo pedido para o cliente cpf: [{cpfOrAnonymous}] - {ex.Message}");
        }
        
    }
}
