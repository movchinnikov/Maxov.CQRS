using System;
using System.Threading;
using System.Threading.Tasks;
using MaxovCQRS.Common;
using MaxovCQRS.Common.Handlers;
using MaxovCQRS.Sample.Command;

namespace MaxovCQRS.Sample.Handlers
{
    public class ReportService :
        IAfterCommandHandler<CreateUserCommand>
    {
        public async Task Execute(CreateUserCommand cmd, ICqrsContext ctx, CancellationToken cancellationToken = new CancellationToken())
        {
            await Task.Delay(20000, cancellationToken);
            Console.WriteLine($"Ура, у нас появился новый пользователь, вот он - {cmd.Login}");
            await Task.CompletedTask;
        }
    }
}