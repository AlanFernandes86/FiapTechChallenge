namespace TechChallenge.Domain.Entities;

public class Payment
{
    public int? Id { get; set; }
    public int OrderId { get; set; }
    public float Value { get; set; }
    public int Method {  get; set; }
    public DateTime? Created_At { get; set; }
}
