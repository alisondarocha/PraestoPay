using Microsoft.EntityFrameworkCore;
using PraestoPay.Domain.AggregatesModel.UserAggregate;

namespace PraestoPay.Infrastructure.Repositories.Identity;

public class UserRepository : IUserRepository //implementar classe repository, com crud padrao.
{
    private readonly PraestoPayContext _context;

    public UserRepository(PraestoPayContext context)
    {
        _context = context;
    }

    public void Add(User entity)
    {
        
    }

    public void Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByExternalId(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync<TId>(TId id)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }
}
