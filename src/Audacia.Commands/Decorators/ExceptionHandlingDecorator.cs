using System;
using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands.Decorators
{
    /// <summary>
    /// <see cref="ICommandHandler{T}"/> implementation to add the functionality to catch exceptions and return the exception details as part of the <see cref="CommandResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the command.</typeparam>
    public class ExceptionHandlingDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _wrappedHandler;

        /// <summary>
        /// Initializes an instance of <see cref="ExceptionHandlingDecorator{T}"/>.
        /// </summary>
        /// <param name="wrappedHandler">The command handler to wrap in exception handling.</param>
        public ExceptionHandlingDecorator(ICommandHandler<T> wrappedHandler)
        {
            _wrappedHandler = wrappedHandler;
        }

        /// <inheritdoc />
        public async Task<CommandResult> HandleAsync(T command, CancellationToken cancellationToken = default)
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                return await _wrappedHandler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                return CommandResult.Failure($"Execution of command {typeof(T).Name} failed due to exception: {exception.Message}");
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }

    /// <summary>
    /// <see cref="ICommandHandler{T,TOutput}"/> implementation to add the functionality to catch exceptions and return the exception details as part of the <see cref="CommandResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the command.</typeparam>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public class ExceptionHandlingDecorator<T, TOutput> : OutputCommandHandlerBase<T, TOutput> where T : ICommand
    {
        private readonly ICommandHandler<T, TOutput> _wrappedHandler;

        /// <summary>
        /// Initializes an instance of <see cref="ExceptionHandlingDecorator{T, TOutput}"/>.
        /// </summary>
        /// <param name="wrappedHandler">The command handler to wrap in exception handling.</param>
        public ExceptionHandlingDecorator(ICommandHandler<T, TOutput> wrappedHandler)
        {
            _wrappedHandler = wrappedHandler;
        }

        /// <inheritdoc />
        public override async Task<CommandResult<TOutput>> HandleAsync(T command, CancellationToken cancellationToken = default)
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                return await _wrappedHandler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                return CommandResult.Failure<TOutput>($"Execution of command {typeof(T).Name} failed due to exception: {exception.Message}");
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}