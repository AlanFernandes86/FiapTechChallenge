using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Repositories;

public interface IPaymentRepository
{
    Task<int> SetPayment(Payment payment);
}
