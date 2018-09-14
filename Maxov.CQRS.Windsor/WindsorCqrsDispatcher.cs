using System.Collections.Generic;
using Castle.Windsor;
using MaxovCQRS.Common.Dispatcher;
using MaxovCQRS.Common.Handlers;

namespace Maxov.CQRS.Windsor
{
    public class WindsorCqrsDispatcher : CqrsDispatcherBase
    {
        private readonly IWindsorContainer _container;

        public WindsorCqrsDispatcher(IWindsorContainer container)
        {
            _container = container;
        }

        protected override IEnumerable<IBeforeCommandHandler<TCommand>> GetBeforeHandlers<TCommand>(TCommand cmd)
        {
            return _container.ResolveAll<IBeforeCommandHandler<TCommand>>();
        }

        protected override ICommandHandler<TCommand> GetHandler<TCommand>(TCommand cmd)
        {
            return _container.Resolve<ICommandHandler<TCommand>>();
        }

        protected override IEnumerable<IAfterCommandHandler<TCommand>> GetAfterHandlers<TCommand>(TCommand cmd)
        {
            return _container.ResolveAll<IAfterCommandHandler<TCommand>>();
        }

        protected override IQueryHandler<TQuery, TResult> GetHandler<TQuery, TResult>(TQuery query)
        {
            return _container.Resolve<IQueryHandler<TQuery, TResult>>();
        }
    }
}