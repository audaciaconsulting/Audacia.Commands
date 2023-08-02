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
        /// <typeparam name="T">The type of the command.</typeparam>
        /// <param name="command">The command to be handled.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="CommandResult"/> object representing the result of the command execution.</returns>
        Task<CommandResult> SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : ICommand;

        /// <summary>
        /// Sends the given <paramref name="command"/> to the appropriate handler to be executed and
        /// returns a <see cref="CommandResult{TOutput}"/> object representing the success or failure of the execution together with an object of type <typeparamref name="TOutput"/>.
        /// </summary>
        /// <typeparam name="T">The type of the command.</typeparam>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="command">The command to be handled.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="CommandResult"/> object representing the result of the command execution.</returns>
        Task<CommandResult<TOutput>> SendAsync<T, TOutput>(T command, CancellationToken cancellationToken = default) where T : ICommand;
    }
}