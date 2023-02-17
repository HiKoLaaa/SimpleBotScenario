namespace SimpleBotScenario.Persistence.Repositories;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal class BotRepository : IBotRepository
{
    private readonly SimpleBotScenarioDbContext _dbContext;

    public BotRepository(SimpleBotScenarioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask CreateBotAsync(Bot bot) => await _dbContext.Bots.AddAsync(bot);

    public async ValueTask<Bot?> GetBotAsync(int id) => await _dbContext.Bots.SingleOrDefaultAsync(bot => bot.Id == id);

    public async ValueTask<Bot?> GetBotByTokenAsync(string token) => await _dbContext.Bots.SingleOrDefaultAsync(bot => bot.Token == token);
}
