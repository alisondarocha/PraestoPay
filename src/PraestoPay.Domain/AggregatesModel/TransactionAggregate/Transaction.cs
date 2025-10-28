using PraestoPay.Domain.AggregatesModel.MerchantAggregate;
using PraestoPay.Domain.AggregatesModel.UserAggregate;
using PraestoPay.Domain.Enums;

namespace PraestoPay.Domain.AggregatesModel.TransactionAggregate;

public class Transaction() : Entity
{
    public Transaction(decimal amount,
                       string currency,
                       PaymentMethodType paymentMethod,
                       Guid userId,
                       Guid merchantId) : this()
    {
        Amount = amount;
        Currency = currency;
        PaymentMethod = paymentMethod;
        UserId = userId;
        MerchantId = merchantId;
        Status = TransactionStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public PaymentMethodType PaymentMethod { get; private set; }
    public Guid UserId { get; private set; }
    public virtual User User { get; private set; }
    public Guid MerchantId { get; private set; }
    public virtual Merchant Merchant { get; private set; }
    public TransactionStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public DateTime? RefundedAt { get; private set; }

    private void Approve()
    {
        if (Status != TransactionStatus.Pending)
            throw new InvalidOperationException("Only pending transactions can be approved.");

        Status = TransactionStatus.Approved;
    }

    private void Reject()
    {
        if (Status != TransactionStatus.Pending)
            throw new InvalidOperationException("Only pending transactions can be rejected.");

        Status = TransactionStatus.Rejected;
    }
    private void Refund()
    {
        if (Status != TransactionStatus.Approved)
            throw new InvalidOperationException("Only approved transactions can be refunded.");

        Status = TransactionStatus.Refunded;
        RefundedAt = DateTime.UtcNow;
    }
}