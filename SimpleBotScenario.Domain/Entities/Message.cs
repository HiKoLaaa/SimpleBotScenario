namespace SimpleBotScenario.Domain.Entities;

using Exceptions;
using Exceptions.Entities;

public class Message
{
    public int Id { get; }

    public int ExternalId { get; }

    public string? Text { get; }

    public Chat Chat { get; }

    public ChatParticipant? ChatParticipant { get; }

    public DateTimeOffset DateTimeUtc { get; }

    public Message(
        int externalId,
        Chat chat,
        DateTimeOffset dateTimeUtc,
        string text,
        ChatParticipant? chatParticipant = null)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new InconsistentEntityStateException(nameof(Message), "отсутствует текст сообщения");

        ExternalId = externalId;
        Chat = chat;
        DateTimeUtc = dateTimeUtc;
        Text = text;
        ChatParticipant = chatParticipant;
    }

#pragma warning disable CS8618
    // Only for EF Core.
    private Message()
#pragma warning restore CS8618
    {
    }
}
