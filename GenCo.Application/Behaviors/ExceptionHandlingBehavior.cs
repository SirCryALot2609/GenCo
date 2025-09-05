using GenCo.Application.DTOs.Common;
using MediatR;

namespace GenCo.Application.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            if (!typeof(TResponse).IsGenericType ||
                typeof(TResponse).GetGenericTypeDefinition() != typeof(BaseResponseDto<>))
                throw; // fallback nếu không phải BaseResponseDto<>
            var failMethod = typeof(BaseResponseDto<>)
                .MakeGenericType(typeof(TResponse).GetGenericArguments()[0])
                .GetMethod(nameof(BaseResponseDto<object>.Fail), new[] { typeof(string), typeof(string) });

            if (failMethod == null) throw; // fallback nếu không phải BaseResponseDto<>
            var response = failMethod.Invoke(null, new object?[] { $"Unexpected error: {ex.Message}", null });
            return (TResponse)response!;

        }
    }
}