namespace SimpleBotScenario.Api.Rest;

using Domain.Exceptions;
using Domain.Exceptions.Entities;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;

public static class ResultExtensions
{
    public static IResult CreateResponse<TResult, TResponse>(this Result<TResult> result, Func<TResult, TResponse>? match = null)
    {
        return result.Match(
            resultObject =>
            {
                if (match is null)
                    return Results.Ok();

                var response = match(resultObject);
                return Results.Ok(response);
            },
            exception =>
            {
                return exception switch
                {
                    EntityException entityNotFoundException => Results.ValidationProblem(RetrieveErrorDictionary(entityNotFoundException)),
                    ValidationException validationException => Results.ValidationProblem(RetrieveErrorDictionary(validationException)),
                    _ => Results.StatusCode(500)
                };
            });
    }

    private static IDictionary<string, string[]> RetrieveErrorDictionary(ValidationException validationException)
    {
        return validationException
            .Errors
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(failure => failure.ErrorMessage).ToArray());
    }

    private static IDictionary<string, string[]> RetrieveErrorDictionary(EntityException entityNotFoundException)
    {
        var errorDictionary = new Dictionary<string, string[]>
        {
            [entityNotFoundException.EntityName] = new[] { entityNotFoundException.Message }
        };

        return errorDictionary;
    }
}
