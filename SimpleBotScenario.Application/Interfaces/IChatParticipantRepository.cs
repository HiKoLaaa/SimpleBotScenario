namespace SimpleBotScenario.Application.Interfaces;

using Domain.Entities;

public interface IChatParticipantRepository
{
    ValueTask<ChatParticipant?> GetChatParticipantsByExternalIdAsync(long externalId);
}