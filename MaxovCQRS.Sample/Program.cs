using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Maxov.CQRS.Windsor;
using MaxovCQRS.Common.Dispatcher;
using MaxovCQRS.Common.Handlers;
using MaxovCQRS.Sample.Commands;
using MaxovCQRS.Sample.Dto;
using MaxovCQRS.Sample.Handlers;
using MaxovCQRS.Sample.Queries;

namespace MaxovCQRS.Sample
{
    public static class Program
    {
        public static void Main()
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<ICommandHandler<CreateUserCommand>, UserService>().Named("CreateUserCommand").LifestyleSingleton(),
                Component.For<IAfterCommandHandler<CreateUserCommand>, NotificationService>().Named("Notify.CreateUserCommand").LifestyleSingleton(),
                Component.For<IAfterCommandHandler<CreateUserCommand>, ReportService>().Named("Report.CreateUserCommand").LifestyleSingleton(),
                Component.For<IQueryHandler<GetUserQuery, UserDto>, UserService>().Named("GetUserQuery").LifestyleSingleton(),
                Component.For<ICqrsDispatcher, WindsorCqrsDispatcher>().LifestyleSingleton(),
                Component.For<IWindsorContainer>().Instance(container)
            );

            var dispatcher = container.Resolve<ICqrsDispatcher>();
            var ctx = new MyCqrsContext(2);
            var createUserCmd = new CreateUserCommand("ivanov.ii", "Иван", "Иванович", "Иванов", "ivanthebest");

            dispatcher.ExecuteCommand(createUserCmd, ctx).GetAwaiter().GetResult();
            var userDto = dispatcher.ExecuteQuery<GetUserQuery, UserDto>(new GetUserQuery(1), ctx).GetAwaiter().GetResult();

            Console.WriteLine($"New user: {userDto}");
            Console.ReadKey();
        }
    }
}