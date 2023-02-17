namespace SimpleBotScenario.Domain.Entities;

using Exceptions;
using Exceptions.Entities;

public class Bot
{
    public int Id { get; set; }

    public string Token { get; set; }

    public ICollection<Chat> Chats { get; set; } = null!;

    public Bot(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new InconsistentEntityStateException(nameof(Bot), "отсутствует токен.");

        Token = token;
    }

#pragma warning disable CS8618
    // Only for EF Core.
    private Bot()
#pragma warning restore CS8618
    {
    }
}
