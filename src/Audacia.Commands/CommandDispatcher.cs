using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands
{
    /// <summary>
    /// Default implementation of <see cref="ICommandDispatcher"/>.
    /// </summary>
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFactory _handlerFactory;

        /// <summary>
        /// Initializes an instance of <see cref="CommandDispatcher"/>.
        /// </summary>
        /// <param name="handlerFactory">The <see cref="ICommandHandlerFactory"/> instance to use to create handlers.</param>
        public CommandDispatcher(ICommandHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }
        
        /// <inheritdoc />
        public async Task<CommandResult> SendAsync<T>(T command, CancellationToken cancellationToken = default) 
            where T : ICommand
        {
            var handler = _handlerFactory.GetHandler<T>(CommandHandlingExecutionMode.FullPipelineOnly);
            if (handler == null)
            {
                throw new CommandWithNoHandlerException<T>();
            }

            return await handler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CommandResult<TOutput>> SendAsync<T, TOutput>(T command, CancellationToken cancellationToken = default) where T : ICommand
        {
            var handler = _handlerFactory.GetHandler<T, TOutput>(CommandHandlingExecutionMode.FullPipelineOnly);
            if (handler == null)
            {
                throw new CommandWithNoHandlerException<T>();
            }

            return await handler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
        }
    }
}