using System;
using System.Threading;
using System.Threading.Tasks;
using MaxovCQRS.Common;
using MaxovCQRS.Common.Handlers;
using MaxovCQRS.Sample.Commands;
using MaxovCQRS.Sample.Dto;
using MaxovCQRS.Sample.Queries;

namespace MaxovCQRS.Sample.Handlers
{
    public class UserService : 
        ICommandHandler<CreateUserCommand>,
        IQueryHandler<GetUserQuery, UserDto>
    {
        public async Task Execute(CreateUserCommand cmd, ICqrsContext ctx, CancellationToken cancellationToken = new CancellationToken())
        {
            cmd.Id = 1;
            Console.WriteLine($"Я создал пользователя: {cmd.Login}");
            await Task.CompletedTask;
        }

        public async Task<UserDto> Execute(GetUserQuery query, ICqrsContext ctx, CancellationToken cancellationToken = new CancellationToken())
        {
            return await Task.FromResult(new UserDto { Id = query.Id, Login = "ivanov.ii", FirstName = "Иван", MiddleName = "Иванович", LastName = "Иванов"});
        }
    }
}