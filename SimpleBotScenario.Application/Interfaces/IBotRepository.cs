namespace SimpleBotScenario.Application.Interfaces;

using Domain.Entities;

public interface IBotRepository
{
    ValueTask CreateBotAsync(Bot bot);

    ValueTask<Bot?> GetBotAsync(int id);

    ValueTask<Bot?> GetBotByTokenAsync(string token);
}
