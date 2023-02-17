namespace SimpleBotScenario.Application.Features.BotEventProcess.Commands;

public class MessageEventProcess
{
    public int Id { get; set; }

    public DateTime DateTimeUtc { get; set; }

    public string? Text { get; set; }

    public long ExternalChatId { get; set; }

    public long ExternalChatParticipantId { get; set; }
}
