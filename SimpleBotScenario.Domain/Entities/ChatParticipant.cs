namespace SimpleBotScenario.Domain.Entities;

public class ChatParticipant
{
    public int Id { get; set; }

    public long ExternalId { get; set; }

    public ICollection<Message> Messages { get; set; } = null!;
}
