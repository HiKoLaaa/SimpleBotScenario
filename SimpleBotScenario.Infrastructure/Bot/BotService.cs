namespace SimpleBotScenario.Infrastructure.Bot;

using Application.Interfaces;
using Configuration;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using DomainMessage = Domain.Entities.Message;

internal class BotService : IBotService
{
    private readonly BotWebhookConfiguration _botWebhookConfiguration;
    private readonly Func<string, ITelegramBotClient> _telegramBotClientFactory;

    public BotService(
        Func<string, ITelegramBotClient> telegramBotClientFactory,
        IOptions<BotWebhookConfiguration> botWebhookConfiguration)
    {
        _telegramBotClientFactory = telegramBotClientFactory;
        _botWebhookConfiguration = botWebhookConfiguration.Value;
    }

    public async Task StartAsync(string token, CancellationToken cancellationToken)
    {
        var telegramBotClient = _telegramBotClientFactory(token);

        await telegramBotClient.SetWebhookAsync(
            $"{string.Format(_botWebhookConfiguration.Url, token)}",
            allowedUpdates: Array.Empty<UpdateType>(),
            secretToken: _botWebhookConfiguration.SecretToken,
            cancellationToken: cancellationToken);
    }

    public async Task<DomainMessage> SendTextMessageAsync(Bot bot, Chat chat, string text)
    {
        var telegramBotClient = _telegramBotClientFactory(bot.Token);

        var sentMessage = await telegramBotClient.SendTextMessageAsync(chat.ExternalId,  text);

        return new DomainMessage(sentMessage.MessageId, chat, sentMessage.Date, text);
    }
}
