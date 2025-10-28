namespace PraestoPay.Domain.AggregatesModel.UserAggregate;

public interface IUserRepository : IRepository<User>
{
    public Task<bool> ExistsAsync();
}
