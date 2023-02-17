namespace SimpleBotScenario.Persistence.Repositories;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal class ChatParticipantRepository : IChatParticipantRepository
{
    private readonly SimpleBotScenarioDbContext _dbContext;

    public ChatParticipantRepository(SimpleBotScenarioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<ChatParticipant?> GetChatParticipantsByExternalIdAsync(long externalId)
    {
        return await _dbContext.ChatParticipants.SingleOrDefaultAsync(chat => chat.ExternalId == externalId);
    }
}