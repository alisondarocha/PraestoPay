namespace PraestoPay.Domain.Enums;

public enum TransactionStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Refunded = 3,
    Failed = 4,
    Cancelled = 5
}
