using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Ports.Services;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;

    public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
    }

    public async Task<int> SetPayment(Payment payment)
    {
        var result = await _paymentRepository.SetPayment(payment);

        if (result != -1)
        {
            await _orderRepository.UpdateOrderStatus(payment.OrderId, Domain.Enums.OrderStatus.RECEIVED);
        }

        return result;
    }
}
