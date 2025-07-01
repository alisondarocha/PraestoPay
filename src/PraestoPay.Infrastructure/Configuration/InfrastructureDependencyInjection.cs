using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PraestoPay.Infrastructure.Configuration;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
