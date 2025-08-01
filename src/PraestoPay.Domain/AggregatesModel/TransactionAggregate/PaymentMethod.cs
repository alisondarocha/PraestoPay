using PraestoPay.Domain.Enums;

namespace PraestoPay.Domain.AggregatesModel.TransactionAggregate;

public class PaymentMethod
{
    public PaymentMethod(PaymentMethodType type, IDictionary<string, string> details)
    {
        Type = type;
        Details = details;
    }

    public PaymentMethodType Type { get; private set; }
    public IDictionary<string, string> Details { get; private set; }
}