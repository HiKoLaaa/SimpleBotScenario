namespace SimpleBotScenario.Application.Interfaces;

public interface IUnitOfWork
{
    IBotRepository BotRepository { get; }

    IChatRepository ChatRepository { get; }

    IChatParticipantRepository ChatParticipantRepository { get; }

    Task SaveAsync();
}
