namespace SimpleBotScenario.Infrastructure;

using Application.Interfaces;
using Bot;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<Func<string, ITelegramBotClient>>(_ => token => new TelegramBotClient(token));
        services.AddTransient<IBotService, BotService>();

        return services;
    }
}
