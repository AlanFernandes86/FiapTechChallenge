namespace TechChallenge.Domain.Enums;

public enum OrderStatus
{
    CREATED = 1,
    RECEIVED = 2,
    IN_PREPARATION = 4,
    READY = 8,
    COMPLETED = 16,
    CANCELLED = 32,
    ACTIVE = RECEIVED | IN_PREPARATION | READY
}
