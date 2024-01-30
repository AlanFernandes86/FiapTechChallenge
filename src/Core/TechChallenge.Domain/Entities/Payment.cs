using TechChallenge.Domain.Schema;

namespace TechChallenge.Domain.Entities;

public class Payment
{
    [SwaggerIgnore]
    public int? Id { get; set; }
    public int OrderId { get; set; }
    public float Value { get; set; }
    public int Method {  get; set; }

    [SwaggerIgnore]
    public DateTime? Created_At { get; set; }
}
