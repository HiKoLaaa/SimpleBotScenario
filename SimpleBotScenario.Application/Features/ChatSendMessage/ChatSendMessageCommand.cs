namespace SimpleBotScenario.Application.Features.ChatSendMessage;

using Behaviors;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

public class ChatSendMessageCommand : IRequest<Result<Unit>>, IResultRequest
{
    public int BotId { get; set; }

    public long ChatId { get; set; }

    public string Text { get; set; } = null!;
}
