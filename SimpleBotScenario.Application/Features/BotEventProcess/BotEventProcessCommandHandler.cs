namespace SimpleBotScenario.Application.Features.BotEventProcess;

using Commands;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.Entities;
using Interfaces;
using JetBrains.Annotations;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Unit = LanguageExt.Unit;

[UsedImplicitly]
internal class ChatActionProcessCommandHandler : IRequestHandler<BotEventProcessCommand, Result<Unit>>
{
    private readonly ILogger<ChatActionProcessCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ChatActionProcessCommandHandler(IUnitOfWork unitOfWork, ILogger<ChatActionProcessCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(BotEventProcessCommand request, CancellationToken cancellationToken)
    {
        var handlingMessageTask = request switch
        {
            { Message: not null } => HandleTextMessage(request),
            { ChannelPost: not null } => HandleChannelPost(request),
            _ => HandleUnprocessableMessage()
        };

        var result = await handlingMessageTask;
        return await result
            .Match(
                async success =>
                {
                    await _unitOfWork.SaveAsync();

                    return new Result<Unit>(success);
                },
                fail => Task.FromResult(new Result<Unit>(fail)));
    }

    private async Task<Result<Unit>> HandleTextMessage(BotEventProcessCommand request)
    {
        var message = request.Message!;
        var chatResult = await GetOrCreateChatAsync(message.ExternalChatId, request.BotToken);
        return await chatResult.Match(
            async chat =>
            {
                var chatParticipant = await _unitOfWork.ChatParticipantRepository.GetChatParticipantsByExternalIdAsync(message.ExternalChatParticipantId) ??
                                      new ChatParticipant { ExternalId = message.ExternalChatParticipantId };

                var newMessage = new Message(message.Id, chat, message.DateTimeUtc, message.Text, chatParticipant);
                return chat
                    .AddUserMessage(newMessage)
                    .Match(success => new Result<Unit>(success), fail => new Result<Unit>(fail));
            },
            fail => Task.FromResult(new Result<Unit>(fail)));
    }

    private async Task<Result<Unit>> HandleChannelPost(BotEventProcessCommand request)
    {
        var channelPost = request.ChannelPost!;
        var chatResult = await GetOrCreateChatAsync(channelPost.ExternalChatId, request.BotToken);
        return await chatResult.Match(
            chat =>
            {
                var newMessage = new Message(channelPost.Id, chat, channelPost.DateTimeUtc, channelPost.Text);

                return chat
                    .AddUserMessage(newMessage)
                    .Match(success => Task.FromResult(new Result<Unit>(success)), fail => Task.FromResult(new Result<Unit>(fail)));
            },
            fail => Task.FromResult(new Result<Unit>(fail)));
    }

    private Task<Result<Unit>> HandleUnprocessableMessage()
    {
        _logger.LogWarning("Сообщение не может быть обработано");

        return Task.FromResult(new Result<Unit>(Unit.Default));
    }

    private async Task<Result<Chat>> GetOrCreateChatAsync(long externalChatId, string botToken)
    {
        var chat = await _unitOfWork.ChatRepository.GetChatByExternalIdAsync(externalChatId);
        if (chat is not null)
            return chat;

        var bot = await _unitOfWork.BotRepository.GetBotByTokenAsync(botToken);

        return bot is null ? new Result<Chat>(new EntityNotFoundException(nameof(Bot))) : new Chat(externalChatId, bot);
    }
}
