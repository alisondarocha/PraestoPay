namespace PraestoPay.Domain.AggregatesModel.MerchantAggregate;

public interface IMerchantRepository : IRepository<Merchant>
{
    public Task<bool> ExistsAsync(Guid id);
}
