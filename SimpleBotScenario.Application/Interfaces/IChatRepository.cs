namespace SimpleBotScenario.Application.Interfaces;

using Domain.Entities;

public interface IChatRepository
{
    ValueTask<Chat?> GetChatByExternalIdAsync(long externalId);

    ValueTask<Chat?> GetChatAsync(long id);
}