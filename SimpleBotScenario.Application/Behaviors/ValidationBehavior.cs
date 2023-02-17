namespace SimpleBotScenario.Application.Behaviors;

using FluentValidation;
using JetBrains.Annotations;
using LanguageExt.Common;
using MediatR;

[UsedImplicitly]
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, IResultRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(result => result.Errors).Where(failure => failure != null).ToList();

        if (failures.Count is 0 || !typeof(TResponse).IsGenericType || typeof(TResponse).GetGenericTypeDefinition() != typeof(Result<>))
            return await next();

        var resultType = typeof(Result<>).MakeGenericType(typeof(TResponse).GenericTypeArguments[0]);
        var response = (TResponse)Activator.CreateInstance(resultType, new ValidationException(failures))!;

        return response;

    }
}
