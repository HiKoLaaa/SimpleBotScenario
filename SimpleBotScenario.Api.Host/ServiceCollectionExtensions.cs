namespace SimpleBotScenario.Api.Host;

using Infrastructure.Bot.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<BotWebhookConfiguration>()
            .Bind(configuration.GetSection("BotWebhook"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}