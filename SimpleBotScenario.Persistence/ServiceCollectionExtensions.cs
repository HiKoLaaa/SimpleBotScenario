namespace SimpleBotScenario.Persistence;

using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using UnitOfWorks;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<SimpleBotScenarioDbContext>(options => { options.UseNpgsql(configuration["ConnectionStrings:Postgres"]); });

        services.AddDatabase();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBotRepository, BotRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IChatParticipantRepository, ChatParticipantRepository>();

        return services;
    }
}
