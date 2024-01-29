namespace TechChallenge.Domain.Entities;

public class Payment
{
    public int? Id { get; set; }
    public int OrderId { get; set; }
    public float Value { get; set; }
    public int Method {  get; set; }
    public DateTime? Created_At { get; set; }

    public Payment(int? id, int orderId, float value, int method, DateTime? created_At)
    {
        Id = id;
        OrderId = orderId;
        Value = value;
        Method = method;
        Created_At = created_At;
    }
}
