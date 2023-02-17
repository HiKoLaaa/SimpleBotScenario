namespace SimpleBotScenario.Infrastructure.Bot.Configuration;

using System.ComponentModel.DataAnnotations;

public class BotWebhookConfiguration
{
    [Required]
    public string Url { get; set; } = null!;

    [Required]
    public string SecretToken { get; set; } = null!;
}