using System.Threading;
using System.Threading.Tasks;
using MaxovCQRS.Common.Primitives;

namespace MaxovCQRS.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand: class, ICommand
    {
        Task Execute(TCommand cmd, ICqrsContext ctx, CancellationToken cancellationToken = new CancellationToken());
    }
}