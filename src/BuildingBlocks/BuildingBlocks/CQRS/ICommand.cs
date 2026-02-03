using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommand : ICommand<Unit> // Empty ICommand that doesnt return any response
    {
    }
    public interface ICommand<out TResponse> : IRequest<TResponse> // ICommand that returns generic response
    {
    }
}
