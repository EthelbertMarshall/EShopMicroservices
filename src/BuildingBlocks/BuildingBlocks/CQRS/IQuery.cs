using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQuery : IQuery<Unit> // Empty IQuery that doesnt return any response
    {
    }

    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull // IQuery that returns generic response
    {
    }

}
