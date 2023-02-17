namespace SimpleBotScenario.Api.Rest.BotEndpoints;

using Application.Features.BotCreate.Commands;
using Application.Features.BotEventProcess.Commands;
using Application.Features.BotStart;
using Application.Features.ChatSendMessage;
using Dtos.Bots;
using Dtos.Chats;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Unit = LanguageExt.Unit;

internal static class BotEndpointsExtensions
{
    public static IEndpointRouteBuilder MapBotEndpoints(this IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup("bots");
        mapGroup
            .MapBotCreate()
            .MapBotGet()
            .MapBotStart()
            .MapBotEvent()
            .MapSendMessage();

        BotRequestMappingRegister.CreateMappings();

        return app;
    }

    private static IEndpointRouteBuilder MapBotCreate(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/",
            async ([FromBody] BotCreateDto createDto, [FromServices] IMediator mediator) =>
            {
                var newBot = await mediator.Send(createDto.Adapt<BotCreateCommand>());

                return newBot.Adapt<BotCreateResponseDto>();
            });

        return app;
    }

    private static IEndpointRouteBuilder MapBotGet(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", () => Console.WriteLine("get"));

        return app;
    }

    private static IEndpointRouteBuilder MapBotStart(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/{id:int}/start",
            async ([FromRoute] int id, [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(id.Adapt<BotStartCommand>());

                return result.CreateResponse<Unit, Unit>();
            });

        return app;
    }

    private static IEndpointRouteBuilder MapBotEvent(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/{token}/events",
            async ([FromRoute] string token, [FromBody] BotEvent botEvent, [FromServices] IMediator mediator) =>
            {
                var botEventProcessCommand = botEvent.Adapt<BotEventProcessCommand>();
                botEventProcessCommand.BotToken = token;

                var result = await mediator.Send(botEventProcessCommand);

                result.CreateResponse(_ => _);
            });

        return app;
    }

    private static IEndpointRouteBuilder MapSendMessage(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/{botId:int}/chats/{chatId:long}/messages/send",
            async (
                [FromRoute] int botId,
                [FromRoute] long chatId,
                [FromBody] ChatSendMessageDto chatSendMessageDto,
                [FromServices] IMediator mediator) =>
            {
                var chatSendMessageCommand = chatSendMessageDto.Adapt<ChatSendMessageCommand>();
                chatSendMessageCommand.BotId = botId;
                chatSendMessageCommand.ChatId = chatId;

                var result = await mediator.Send(chatSendMessageCommand);

                return result.CreateResponse(_ => _);
            });

        return app;
    }
}
