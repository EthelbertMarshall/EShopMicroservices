using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request); //Context object created

            var results = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

            var failures = results
                           .Where( e => e.Errors.Any())
                           .SelectMany(e => e.Errors)
                           .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
