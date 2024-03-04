using FluentValidation;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.UpdateOrderStatus
{
    public class UpdateOrderStatusUseCase : IUseCase<UpdateOrderStatusDAO, UseCaseOutput<int>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<UpdateOrderStatusDAO> _validator;

        public UpdateOrderStatusUseCase(IOrderRepository orderRepository, IValidator<UpdateOrderStatusDAO> validator)
        {
            _orderRepository = orderRepository;
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

                return new UseCaseOutput<int>(orderId);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<int>($"Erro ao atualizar status do pedido id: [{input.OrderId}] - {ex.Message}");
            }            
        }
    }
}
