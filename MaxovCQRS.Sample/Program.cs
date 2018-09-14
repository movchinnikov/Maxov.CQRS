using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Maxov.CQRS.Windsor;
using MaxovCQRS.Common.Dispatcher;
using MaxovCQRS.Common.Handlers;
using MaxovCQRS.Sample.Command;
using MaxovCQRS.Sample.Handlers;

namespace MaxovCQRS.Sample
{
    public static class Program
    {
        public static void Main()
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<ICommandHandler<CreateUserCommand>, UserService>().LifestyleSingleton(),
                Component.For<IAfterCommandHandler<CreateUserCommand>, NotificationService>().LifestyleSingleton(),
                Component.For<IAfterCommandHandler<CreateUserCommand>, ReportService>().LifestyleSingleton(),
                Component.For<ICqrsDispatcher, WindsorCqrsDispatcher>().LifestyleSingleton(),
                Component.For<IWindsorContainer>().Instance(container)
            );

            var dispatcher = container.Resolve<ICqrsDispatcher>();

            var createUserCmd = new CreateUserCommand("ivanov.ii", "Иван", "Иванович", "Иванов", "ivanthebest");

            dispatcher.ExecuteCommand(createUserCmd, new MyCqrsContext(2)).GetAwaiter().GetResult();

            Console.WriteLine("Hello, world!");
            Console.ReadKey();
        }
    }
}