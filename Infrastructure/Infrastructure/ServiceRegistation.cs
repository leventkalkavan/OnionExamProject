using Application.Abstractions.Token;
using Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistation
{
    public static void AddInfrastructureServices(this IServiceCollection service)
    {
        service.AddScoped<ITokenHandler, TokenHandler>();
    }
}