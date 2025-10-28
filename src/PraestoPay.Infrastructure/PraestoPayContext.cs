using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PraestoPay.Domain.AggregatesModel;
using System.Linq.Expressions;

namespace PraestoPay.Infrastructure;

public class PraestoPayContext : DbContext, IDbContext
{
    public PraestoPayContext(DbContextOptions<PraestoPayContext> options)
        : base(options)
    {
    }

    DatabaseFacade IDbContext.Database => Database;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PraestoPayContext).Assembly);

        RegisterEntitiesFromDomainAssembly(modelBuilder);

        ApplyGlobalSoftDeleteFilter(modelBuilder);
    }

    private void RegisterEntitiesFromDomainAssembly(ModelBuilder modelBuilder)
    {
        var domainAssembly = typeof(Entity).Assembly;

        var entityTypes = domainAssembly.GetTypes()
            .Where(t => t.IsClass
                && !t.IsAbstract
                && t.IsSubclassOf(typeof(Entity)))
            .ToList();

        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType);
        }
    }

    private void ApplyGlobalSoftDeleteFilter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(Entity.IsDeleted));
                var filter = Expression.Lambda(Expression.Not(property), parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateTimestamp();
                    break;

                case EntityState.Modified:
                    if (!entry.Entity.IsDeleted)
                    {
                        entry.Entity.UpdateTimestamp();
                    }
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}