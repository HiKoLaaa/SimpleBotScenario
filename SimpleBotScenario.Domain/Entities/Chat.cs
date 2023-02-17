namespace SimpleBotScenario.Domain.Entities;

using Exceptions;
using Exceptions.Entities;
using LanguageExt;
using LanguageExt.Common;

public class Chat
{
    public long Id { get; private set; }

    public long ExternalId { get; private set; }

    public Bot Bot { get; private set; }

    public ICollection<Message> Messages { get; private set; } = new List<Message>();

    public Chat(long externalId, Bot bot)
    {
        ExternalId = externalId;
        Bot = bot;
    }

#pragma warning disable CS8618
    // Only for EF Core.
    private Chat()
#pragma warning restore CS8618
    {
    }

    public Result<Unit> AddUserMessage(Message message)
    {
        if (message.ChatParticipant is null)
            return new Result<Unit>(new InconsistentEntityStateException(nameof(Message), "отсутствует пользователь, отправивший сообщение"));

        Messages.Add(message);

        return new Result<Unit>(Unit.Default);
    }

    public Result<Unit> AddBotMessage(Message message)
    {
        if (message.ChatParticipant is not null)
            return new Result<Unit>(new InconsistentEntityStateException(nameof(Message), "бот не является пользователем, отправляющий сообщение"));

        Messages.Add(message);

        return new Result<Unit>(Unit.Default);
    }
}
