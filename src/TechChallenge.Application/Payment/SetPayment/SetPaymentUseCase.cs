using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.SetPayment
{
    public class SetPaymentUseCase : IUseCase<SetPaymentDAO, UseCaseOutput<int>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;

        public SetPaymentUseCase(
            IOrderRepository orderRepository,
            IPaymentRepository paymentRepository
        )
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<UseCaseOutput<int>> Handle(SetPaymentDAO input, CancellationToken cancellationToken)
        {
            try
            {
                var payment = new Payment
                {
                    OrderId = input.OrderId,
                    Value = input.Value,
                    Method = input.Method
                };

                var result = await _paymentRepository.SetPayment(payment);

                await _orderRepository.UpdateOrderStatus(input.OrderId, Domain.Enums.OrderStatus.RECEIVED);

                return new UseCaseOutput<int>(result);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<int>(errorMessage: $"Erro ao registrar pagamento OrderId: {input.OrderId} - {ex.Message}");
            }            
        }
    }
}
