using System.Threading;
using System.Threading.Tasks;
using MaxovCQRS.Common.Primitives;

namespace MaxovCQRS.Common.Handlers
{
    public interface IAfterQueryHandler<in TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        Task<TResult> Execute(TQuery query, ICqrsContext ctx, CancellationToken cancellationToken = new CancellationToken());
    }
}