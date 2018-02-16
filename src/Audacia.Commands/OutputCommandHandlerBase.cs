using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands
{
    /// <summary>
    /// Base class to be inherited by <see cref="ICommandHandler{T}"/> implementations to allow them to be called if the client only has
    /// an instance of <see cref="ICommandHandler{T}"/> (if they are not interested in the value of <see cref="ICommandHandler{T}"/>.
    /// Inheriting this base class means that implementations do not need to explicitly implement <see cref="ICommandHandler{T}"/> themselves.
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public abstract class OutputCommandHandlerBase<T, TOutput> : ICommandHandler<T>, ICommandHandler<T, TOutput> where T : ICommand
    {
        /// <summary>
        /// Explicit implementation of <see cref="ICommandHandler{T}"/> to allow command to be handled if the client only has
        /// an instance of <see cref="ICommandHandler{T}"/> rather than <see cref="ICommandHandler{T,TOutput}"/>.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task<CommandResult> ICommandHandler<T>.HandleAsync(T command, CancellationToken cancellationToken)
        {
            return await HandleAsync(command, cancellationToken);
        }

        /// <summary>
        /// When overridden in a subclass, handles the given <paramref name="command"/>.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<CommandResult<TOutput>> HandleAsync(T command, CancellationToken cancellationToken = default(CancellationToken));
    }
}