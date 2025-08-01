namespace PraestoPay.Domain.AggregatesModel.MerchantAggregate;
public class Merchant() : Entity
{
    public Merchant(string name, string email, string merchantCode) : this()
    {
        Name = name;
        Email = email;
        MerchantCode = merchantCode;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string MerchantCode { get; private set; }
}
