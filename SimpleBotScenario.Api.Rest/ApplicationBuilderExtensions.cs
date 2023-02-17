namespace SimpleBotScenario.Api.Rest;

using BotEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

public static class ApplicationBuilderExtensions
{
    public static IEndpointRouteBuilder MapRest(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api");
        api
            .MapBotEndpoints();

        return api;
    }
}
