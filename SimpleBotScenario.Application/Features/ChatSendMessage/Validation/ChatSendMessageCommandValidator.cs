namespace SimpleBotScenario.Application.Features.ChatSendMessage.Validation;

using FluentValidation;
using JetBrains.Annotations;

[UsedImplicitly]
public class ChatSendMessageCommandValidator : AbstractValidator<ChatSendMessageCommand>
{
    public ChatSendMessageCommandValidator()
    {
        RuleFor(command => command.Text).NotEmpty().WithMessage("Текст должен быть указан");
    }
}
