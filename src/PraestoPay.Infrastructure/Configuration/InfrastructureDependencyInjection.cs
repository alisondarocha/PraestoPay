using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PraestoPay.Domain.AggregatesModel;

namespace PraestoPay.Infrastructure.Configuration;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddDatabaseContext(services, configuration);

        AddRepositories(services);

        return services;
    }

    private static void AddDatabaseContext(
        IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                "Database connection string 'DefaultConnection' is not configured. " +
                "Please set it in User Secrets (Development) or Environment Variables (Production). " +
                "Run: dotnet user-secrets set \"ConnectionStrings:DefaultConnection\" \"your-connection-string\"");

        services.AddDbContext<PraestoPayContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(PraestoPayContext).Assembly.FullName);

                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);

                npgsqlOptions.CommandTimeout(30);

            });

            var enableSensitiveLogging = configuration.GetValue<bool>("Logging:EnableSensitiveDataLogging");
            var enableDetailedErrors = configuration.GetValue<bool>("Logging:EnableDetailedErrors");

            if (enableSensitiveLogging)
                options.EnableSensitiveDataLogging();

            if (enableDetailedErrors)
                options.EnableDetailedErrors();

            if (configuration.GetValue<bool>("Logging:LogSqlToConsole"))
                options.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        });

        services.AddScoped<IDbContext>(provider =>
            provider.GetRequiredService<PraestoPayContext>());
    }

    private static void AddRepositories(IServiceCollection services)
    {
        var infrastructureAssembly = typeof(InfrastructureDependencyInjection).Assembly;

        var domainAssembly = typeof(IRepository<>).Assembly;

        var repositoryInterfaces = domainAssembly.GetTypes()
            .Where(t => t.IsInterface &&
                   (t.Name.EndsWith("Repository") || t.GetInterfaces().Any(i =>
                       i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>))))
            .ToList();

        var repositoryImplementations = infrastructureAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"))
            .ToList();

        var registeredCount = 0;

        foreach (var implementation in repositoryImplementations)
        {
            var interfaces = implementation.GetInterfaces()
                .Where(i => repositoryInterfaces.Contains(i))
                .ToList();

            foreach (var @interface in interfaces)
            {
                services.AddScoped(@interface, implementation);
                registeredCount++;
                Console.WriteLine($"Registered: {@interface.Name} -> {implementation.Name}");
            }
        }

        if (registeredCount == 0)
            Console.WriteLine("Warning: No repositories were automatically registered. " +
                            "Ensure your repositories follow naming conventions.");
        else
            Console.WriteLine($"Total repositories registered: {registeredCount}");
    }

    private static void AddCaching(
        IServiceCollection services,
        IConfiguration configuration)
    {
        var redisConnection = configuration.GetConnectionString("Redis");

        if (!string.IsNullOrWhiteSpace(redisConnection))
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
                options.InstanceName = "PraestoPay:";
            });
    }

    private static void AddMessageBus(
        IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitMqConnection = configuration.GetConnectionString("RabbitMQ");

        if (!string.IsNullOrWhiteSpace(rabbitMqConnection))
        {
            // TODO: Configure MassTransit with RabbitMQ}
        }
    }

    private static void AddExternalServices(
        IServiceCollection services,
        IConfiguration configuration)
    {
        // TODO: Add when implementing payment processing
    }
}