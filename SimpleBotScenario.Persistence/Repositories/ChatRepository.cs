namespace SimpleBotScenario.Persistence.Repositories;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal class ChatRepository : IChatRepository
{
    private readonly SimpleBotScenarioDbContext _dbContext;

    public ChatRepository(SimpleBotScenarioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Chat?> GetChatByExternalIdAsync(long externalId)
    {
        return await _dbContext
            .Chats
            .Include(chat => chat.Messages)
            .SingleOrDefaultAsync(chat => chat.ExternalId == externalId);
    }

    public async ValueTask<Chat?> GetChatAsync(long id)
    {
        return await _dbContext
            .Chats
            .Include(chat => chat.Messages)
            .SingleOrDefaultAsync(chat => chat.Id == id);
    }
}
