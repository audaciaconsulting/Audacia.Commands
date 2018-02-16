using System;
using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands.Decorators
{
    /// <summary>
    /// <see cref="ICommandHandler{T}"/> implementation to add the functionality to catch exceptions and return the exception details as part of the <see cref="CommandResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExceptionHandlingDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _wrappedHandler;

        public ExceptionHandlingDecorator(ICommandHandler<T> wrappedHandler)
        {
            _wrappedHandler = wrappedHandler;
        }

        public async Task<CommandResult> HandleAsync(T command, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                return await _wrappedHandler.HandleAsync(command, cancellationToken);
            }
            catch (Exception exception)
            {
                return CommandResult.Failure($"Execution of command {typeof(T).Name} failed due to exception: {exception.Message}");
            }
        }
    }

    /// <summary>
    /// <see cref="ICommandHandler{T,TOutput}"/> implementation to add the functionality to catch exceptions and return the exception details as part of the <see cref="CommandResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public class ExceptionHandlingDecorator<T, TOutput> : OutputCommandHandlerBase<T, TOutput> where T : ICommand
    {
        private readonly ICommandHandler<T, TOutput> _wrappedHandler;

        public ExceptionHandlingDecorator(ICommandHandler<T, TOutput> wrappedHandler)
        {
            _wrappedHandler = wrappedHandler;
        }

        public override async Task<CommandResult<TOutput>> HandleAsync(T command, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                return await _wrappedHandler.HandleAsync(command, cancellationToken);
            }
            catch (Exception exception)
            {
                return CommandResult.Failure<TOutput>($"Execution of command {typeof(T).Name} failed due to exception: {exception.Message}");
            }
        }
    }
}