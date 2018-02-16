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
    /// Abstraction to be implemented by classes that can handle a command of type <see cref="T"/> and just return a basic <see cref="CommandResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommandHandler<in T> : ICommandHandler where T : ICommand
    {
        /// <summary>
        /// Handles the given <paramref name="command"/> asynchronously and returns a <see cref="CommandResult"/>
        /// representing the success or failure of the operation.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CommandResult> HandleAsync(T command, CancellationToken cancellationToken = default(CancellationToken));
    }

    /// <summary>
    /// Abstraction to be implemented by classes that can handle a command of type <see cref="T"/> and return a result of type <see cref="TOutput"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface ICommandHandler<in T, TOutput> : ICommandHandler where T : ICommand
    {
        /// <summary>
        /// Handles the given <paramref name="command"/> asynchronously and returns a <see cref="CommandResult{TOutput}"/>
        /// representing the success or failure of the operation and containing an object of type <see cref="TOutput"/>.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CommandResult<TOutput>> HandleAsync(T command, CancellationToken cancellationToken = default(CancellationToken));
    }
}