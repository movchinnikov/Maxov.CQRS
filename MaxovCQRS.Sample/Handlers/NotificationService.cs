using System;
using System.Threading;
using System.Threading.Tasks;
using MaxovCQRS.Common;
using MaxovCQRS.Common.Handlers;
using MaxovCQRS.Sample.Commands;

namespace MaxovCQRS.Sample.Handlers
{
    public class NotificationService :
        IAfterCommandHandler<CreateUserCommand>
    {
        public async Task Execute(CreateUserCommand cmd, ICqrsContext ctx, CancellationToken cancellationToken = new CancellationToken())
        {
            await Task.Delay(10000, cancellationToken);
            Console.WriteLine($"Отправили пользователю {cmd.Login} смс о регистрации");
            await Task.CompletedTask;
        }
    }
}