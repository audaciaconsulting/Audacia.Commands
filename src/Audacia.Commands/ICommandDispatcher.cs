using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands
{
    /// <summary>
    /// Abstraction to represent the dispatching of commands to the appropriate handler.
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Sends the given <paramref name="command"/> to the appropriate handler to be executed and
        /// returns a <see cref="CommandResult"/> object representing the success or failure of the execution.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CommandResult> SendAsync<T>(T command, CancellationToken cancellationToken = default(CancellationToken)) where T : ICommand;

        /// <summary>
        /// Sends the given <paramref name="command"/> to the appropriate handler to be executed and
        /// returns a <see cref="CommandResult{TOutput}"/> object representing the success or failure of the execution together with an object of type <see cref="TOutput"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CommandResult<TOutput>> SendAsync<T, TOutput>(T command, CancellationToken cancellationToken = default(CancellationToken)) where T : ICommand;
    }
}