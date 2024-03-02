using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Ports.Repositories;

public interface IPaymentRepository
{
    Task<int> SetPayment(Payment payment);
}
