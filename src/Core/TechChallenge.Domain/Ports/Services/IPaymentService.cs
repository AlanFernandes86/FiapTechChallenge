using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Ports.Services;

public interface IPaymentService
{
    Task<int> SetPayment(Payment payment);
}
