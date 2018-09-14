using System;
using System.Threading;
using System.Threading.Tasks;
using MaxovCQRS.Common;
using MaxovCQRS.Common.Handlers;
using MaxovCQRS.Sample.Command;

namespace MaxovCQRS.Sample.Handlers
{
    public class UserService : ICommandHandler<CreateUserCommand>
    {
        public async Task Execute(CreateUserCommand cmd, ICqrsContext ctx, CancellationToken cancellationToken = new CancellationToken())
        {
            cmd.Id = 1;
            Console.WriteLine($"Я создал пользователя: {cmd.Login}");
            await Task.CompletedTask;
        }
    }
}