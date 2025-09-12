using FluentValidation;
using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count == 0)
        {
            return await next();
        }

        if (!typeof(TResponse).IsGenericType ||
            typeof(TResponse).GetGenericTypeDefinition() != typeof(BaseResponseDto<>))
            throw new ValidationException(failures);

        var errorMessages = failures.Select(f => f.ErrorMessage).ToList();
        var responseType = typeof(TResponse).GetGenericArguments()[0];
        var method = typeof(BaseResponseDto<>)
            .MakeGenericType(responseType)
            .GetMethod(
                nameof(BaseResponseDto<object>.Fail),
                [typeof(IEnumerable<string>), typeof(string)]
            );

        return (TResponse)method!.Invoke(
            null,
            [errorMessages, "VALIDATION_ERROR"]
        )!;
    }
}
