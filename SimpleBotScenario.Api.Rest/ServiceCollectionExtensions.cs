namespace SimpleBotScenario.Api.Rest;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRest(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));

        return services;
    }
}
