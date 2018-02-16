using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFactory _handlerFactory;

        public CommandDispatcher(ICommandHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }
        
        public async Task<CommandResult> SendAsync<T>(T command, CancellationToken cancellationToken = new CancellationToken()) 
            where T : ICommand
        {
            var handler = _handlerFactory.GetHandler<T>(CommandHandlingExecutionMode.FullPipelineOnly);
            if (handler == null)
            {
                throw new CommandWithNoHandlerException<T>();
            }

            return await handler.HandleAsync(command, cancellationToken);
        }

        public async Task<CommandResult<TOutput>> SendAsync<T, TOutput>(T command, CancellationToken cancellationToken = new CancellationToken()) where T : ICommand
        {
            var handler = _handlerFactory.GetHandler<T, TOutput>(CommandHandlingExecutionMode.FullPipelineOnly);
            if (handler == null)
            {
                throw new CommandWithNoHandlerException<T>();
            }

            return await handler.HandleAsync(command, cancellationToken);
        }
    }
}