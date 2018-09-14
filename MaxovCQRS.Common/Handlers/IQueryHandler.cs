using System.Threading;
using System.Threading.Tasks;
using MaxovCQRS.Common.Primitives;

namespace MaxovCQRS.Common.Handlers
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        Task<TResult> Execute(TQuery cmd, ICqrsContext ctx, CancellationToken cancellationToken = new CancellationToken());
    }
}