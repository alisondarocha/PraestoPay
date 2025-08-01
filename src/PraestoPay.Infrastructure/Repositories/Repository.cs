using Microsoft.EntityFrameworkCore;
using PraestoPay.Domain.AggregatesModel;

namespace PraestoPay.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly IDbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(IDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }

    public virtual void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public async Task<TEntity> GetByIdAsync<TId>(TId id)
    {
        return await DbSet.FindAsync(id) 
               ?? throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with ID {id} not found.");
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
}
