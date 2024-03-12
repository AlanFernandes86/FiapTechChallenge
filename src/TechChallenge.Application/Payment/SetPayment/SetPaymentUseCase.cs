using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Application.Order.UpdateOrderStatus;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.SetPayment
{
    public class SetPaymentUseCase : IUseCase<SetPaymentDAO, UseCaseOutput<int>>
    {
        private readonly IUseCase<UpdateOrderStatusDAO, UseCaseOutput<int>> _updateOrderStatusUseCase;
        private readonly IPaymentRepository _paymentRepository;

        public SetPaymentUseCase(
            IUseCase<UpdateOrderStatusDAO, UseCaseOutput<int>> updateOrderStatusUseCase,
            IPaymentRepository paymentRepository
        )
        {
            _updateOrderStatusUseCase = updateOrderStatusUseCase;
            _paymentRepository = paymentRepository;
        }

        public async Task<UseCaseOutput<int>> Handle(SetPaymentDAO input)
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

                _ = UpdateOrderStatus(result);

                return new UseCaseOutput<int>(result);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<int>(errorMessage: $"Erro ao registrar pagamento OrderId: {input.OrderId} - {ex.Message}");
            }            
        }

        private async Task UpdateOrderStatus(int orderId)
        {
            var useCaseDao = new UpdateOrderStatusDAO(orderId, Domain.Enums.OrderStatus.RECEIVED);
            await _updateOrderStatusUseCase.Handle(useCaseDao);
        }
    }
}
