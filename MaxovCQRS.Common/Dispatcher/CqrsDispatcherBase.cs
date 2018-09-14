using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MaxovCQRS.Common.Handlers;
using MaxovCQRS.Common.Primitives;

namespace MaxovCQRS.Common.Dispatcher
{
    public abstract class CqrsDispatcherBase : ICqrsDispatcher
    {
        public async Task ExecuteCommand<TCommand>(TCommand cmd, ICqrsContext ctx, 
            CancellationToken cancellationToken = new CancellationToken()) where TCommand : class, ICommand
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd), "Команда не передана");

            await Before<TCommand>(cmd, ctx, cancellationToken);

            await Proceed<TCommand>(cmd, ctx, cancellationToken);

            await After<TCommand>(cmd, ctx, cancellationToken);
        }

        public async Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query, ICqrsContext ctx,
            CancellationToken cancellationToken = new CancellationToken()) where TQuery : class, IQuery<TResult>
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query), "Запрос не передан");

            var result = await Proceed<TQuery, TResult>(query, ctx, cancellationToken);

            return result;
        }

        protected abstract IEnumerable<IBeforeCommandHandler<TCommand>> GetBeforeHandlers<TCommand>(TCommand cmd) where TCommand : class, ICommand;
        protected abstract ICommandHandler<TCommand> GetHandler<TCommand>(TCommand cmd) where TCommand : class, ICommand;
        protected abstract IEnumerable<IAfterCommandHandler<TCommand>> GetAfterHandlers<TCommand>(TCommand cmd) where TCommand : class, ICommand;

        protected virtual async Task Before<TCommand>(TCommand cmd, ICqrsContext ctx, CancellationToken cancellationToken) where TCommand : class, ICommand
        {
            var handlers = GetBeforeHandlers(cmd);

            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    if (handler == null) continue;
                    await handler.Execute(cmd, ctx, cancellationToken);
                }
            }
        }

        protected virtual async Task Proceed<TCommand>(TCommand cmd, ICqrsContext ctx,CancellationToken cancellationToken) where TCommand : class, ICommand
        {
            var handler = GetHandler<TCommand>(cmd);

            if (handler == null)
                throw new NotImplementedException($"Не определен обработчик для команды {cmd.GetType()}");

            await handler.Execute(cmd, ctx, cancellationToken);
        }

        protected virtual async Task After<TCommand>(TCommand cmd, ICqrsContext ctx, CancellationToken cancellationToken) where TCommand : class, ICommand
        {
            var handlers = GetAfterHandlers(cmd);

            if (handlers != null)
            {
                var tasks = new List<Task>();

                foreach (var handler in handlers)
                {
                    if (handler == null) continue;
                    tasks.Add(handler.Execute(cmd, ctx, cancellationToken));
                }

                Task.WaitAll(tasks.ToArray());
            }

            await Task.CompletedTask;
        }

        //protected abstract IEnumerable<IBeforeQueryHandler<TQuery, TResult>> GetBeforeHandlers<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
        protected abstract IQueryHandler<TQuery, TResult> GetHandler<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
        //protected abstract IEnumerable<IAfterQueryHandler<TQuery, TResult>> GetAfterHandlers<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;

        protected virtual async Task<TResult> Proceed<TQuery, TResult>(TQuery query, ICqrsContext ctx, CancellationToken cancellationToken) where TQuery : class, IQuery<TResult>
        {
            var handler = GetHandler<TQuery, TResult>(query);

            if (handler == null)
                throw new NotImplementedException($"Не определен обработчик для запроса {query.GetType()}");

            return await handler.Execute(query, ctx, cancellationToken);
        }
    }
}