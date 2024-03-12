using TechChallenge.Application.Common.UseCase.Interfaces;

namespace TechChallenge.Application.Order.SetPayment;

public class SetPaymentDAO : IUseCaseDAO
{
    public int OrderId { get; set; }
    public float Value { get; set; }
    public int Method { get; set; }
}
