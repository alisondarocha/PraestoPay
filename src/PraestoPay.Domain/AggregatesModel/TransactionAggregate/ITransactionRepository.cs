namespace PraestoPay.Domain.AggregatesModel.TransactionAggregate;

public interface ITransactionRepository : IRepository<Transaction>
{
    public Task<bool> ExistsAsync(Guid id);
}
