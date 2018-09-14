using System.Threading;
using System.Threading.Tasks;
using MaxovCQRS.Common.Primitives;

namespace MaxovCQRS.Common.Dispatcher
{
    public interface ICqrsDispatcher
    {
        Task ExecuteCommand<TCommand>(TCommand cmd, ICqrsContext ctx, 
            CancellationToken cancellationToken = new CancellationToken()) where TCommand : class, ICommand;

        Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query, ICqrsContext ctx, 
            CancellationToken cancellationToken = new CancellationToken()) where TQuery : class, IQuery<TResult>;
    }
}