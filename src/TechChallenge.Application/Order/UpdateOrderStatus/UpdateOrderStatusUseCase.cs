using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Application.Hubs;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.UpdateOrderStatus
{
    public class UpdateOrderStatusUseCase : IUseCase<UpdateOrderStatusDAO, UseCaseOutput<int>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHubContext<OrderHub> _hubContext;
        private readonly IValidator<UpdateOrderStatusDAO> _validator;

        public UpdateOrderStatusUseCase(
            IOrderRepository orderRepository,
            IHubContext<OrderHub> hubContext,
            IValidator<UpdateOrderStatusDAO> validator
            )
        {
            _orderRepository = orderRepository;
            _hubContext = hubContext;
            _validator = validator;
        }

        public async Task<UseCaseOutput<int>> Handle(UpdateOrderStatusDAO input)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(input);

                if (!validationResult.IsValid)
                {
                    return new UseCaseOutput<int>(new Validation(validationResult.Errors.FirstOrDefault()?.ErrorCode!, validationResult.Errors.FirstOrDefault()?.ErrorMessage!));
                }

                var orderId = await _orderRepository.UpdateOrderStatus(input.OrderId, input.OrderStatus);

                if (orderId == -1)
                {
                    return new UseCaseOutput<int>(new Validation("ORDER_NOT_FOUND", $"pedido id: [{input.OrderId}] - não encontrado"));
                }

                _ = SendActiveOrders();

                return new UseCaseOutput<int>(orderId);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<int>($"Erro ao atualizar status do pedido id: [{input.OrderId}] - {ex.Message}");
            }            
        }

        private async Task SendActiveOrders()
        {
            try
            {
                var activeOrders = await _orderRepository.GetOrdersByStatus(OrderStatus.ACTIVE);

                await _hubContext.Clients.All.SendAsync("ActiveOrders", activeOrders);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}
