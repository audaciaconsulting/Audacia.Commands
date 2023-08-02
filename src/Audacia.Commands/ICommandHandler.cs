using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands
{
    /// <summary>
    /// Marker interface to be inherited by <see cref="ICommandHandler{T}"/> and <see cref="ICommandHandler{T,TResult}"/>.
    /// Used for IoC purposes.
    /// </summary>
    public interface ICommandHandler
    {
    }

    /// <summary>
    /// Abstraction to be implemented by classes that can handle a command of type <typeparamref name="T"/> and just return a basic <see cref="CommandResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the command.</typeparam>
    public interface ICommandHandler<in T> : ICommandHandler where T : ICommand
    {
        /// <summary>
        /// Handles the given <paramref name="command"/> asynchronously and returns a <see cref="CommandResult"/>
        /// representing the success or failure of the operation.
        /// </summary>
        /// <param name="command">The command to be handled.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="CommandResult"/> object representing the result of the command execution.</returns>
        Task<CommandResult> HandleAsync(T command, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Abstraction to be implemented by classes that can handle a command of type <typeparamref name="T"/> and return a result of type <typeparamref name="TOutput"/>.
    /// </summary>
    /// <typeparam name="T">The type of the command.</typeparam>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public interface ICommandHandler<in T, TOutput> : ICommandHandler where T : ICommand
    {
        /// <summary>
        /// Handles the given <paramref name="command"/> asynchronously and returns a <see cref="CommandResult{TOutput}"/>
        /// representing the success or failure of the operation and containing an object of type <typeparamref name="TOutput"/>.
        /// </summary>
        /// <param name="command">The command to be handled.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="CommandResult{TOutput}"/> object representing the result of the command execution.</returns>
        Task<CommandResult<TOutput>> HandleAsync(T command, CancellationToken cancellationToken = default);
    }
}