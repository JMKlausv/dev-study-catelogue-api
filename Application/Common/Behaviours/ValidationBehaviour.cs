using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = Application.Common.Exceptions.ValidationException;


namespace Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators , ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
    {
        _validators = validators;
       _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {

        var context = new ValidationContext<TRequest>(request);
        var failures = _validators.Select(v => v.Validate(context))
                                  .SelectMany(result => result.Errors)
                                  .Where(f => f != null)
                                  .ToList();
        _logger.LogError("what is happening error-----n " + string.Join("::", failures));
        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next();
    }
}