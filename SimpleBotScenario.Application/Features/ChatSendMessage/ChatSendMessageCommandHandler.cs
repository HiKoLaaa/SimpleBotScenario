namespace SimpleBotScenario.Application.Features.ChatSendMessage;

using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.Entities;
using Interfaces;
using JetBrains.Annotations;
using LanguageExt;
using LanguageExt.Common;

[UsedImplicitly]
public class ChatSendMessageCommandHandler : MediatR.IRequestHandler<ChatSendMessageCommand, Result<Unit>>
{
    private readonly IBotService _botService;
    private readonly IUnitOfWork _unitOfWork;

    public ChatSendMessageCommandHandler(IUnitOfWork unitOfWork, IBotService botService)
    {
        _unitOfWork = unitOfWork;
        _botService = botService;
    }

    public async Task<Result<Unit>> Handle(ChatSendMessageCommand request, CancellationToken cancellationToken)
    {
        var bot = await _unitOfWork.BotRepository.GetBotAsync(request.BotId);
        if (bot is null)
            return new Result<Unit>(new EntityNotFoundException(nameof(Bot)));

        var chat = await _unitOfWork.ChatRepository.GetChatAsync(request.ChatId);
        if (chat is null)
            return new Result<Unit>(new EntityNotFoundException(nameof(Chat)));

        var message = await _botService.SendTextMessageAsync(bot, chat, request.Text);

        return await chat
            .AddBotMessage(message)
            .Match(
                async success =>
                {
                    await _unitOfWork.SaveAsync();

                    return new Result<Unit>(success);
                },
                fail => Task.FromResult(new Result<Unit>(fail)));
    }
}
