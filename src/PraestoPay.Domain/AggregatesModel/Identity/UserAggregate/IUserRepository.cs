namespace PraestoPay.Domain.AggregatesModel.Identity.UserAggregate;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByExternalId(Guid id);
}
