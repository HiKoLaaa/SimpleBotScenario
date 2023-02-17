namespace SimpleBotScenario.Application.Interfaces;

using Domain.Entities;

public interface IBotService
{
    Task StartAsync(string token, CancellationToken cancellationToken);

    Task<Message> SendTextMessageAsync(Bot bot, Chat chat, string text);
}