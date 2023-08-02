using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands
{
    /// <summary>
    /// Base class to be inherited by <see cref="ICommandHandler{T}"/> implementations to allow them to be called if the client only has
    /// an instance of <see cref="ICommandHandler{T}"/> (if they are not interested in the value of <see cref="ICommandHandler{T}"/>.
    /// Inheriting this base class means that implementations do not need to explicitly implement <see cref="ICommandHandler{T}"/> themselves.
    /// </summary>
    /// <typeparam name="T">The type of the command.</typeparam>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public abstract class OutputCommandHandlerBase<T, TOutput> : ICommandHandler<T>, ICommandHandler<T, TOutput> where T : ICommand
    {
        /// <inheritdoc />
        async Task<CommandResult> ICommandHandler<T>.HandleAsync(T command, CancellationToken cancellationToken)
        {
            return await HandleAsync(command, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public abstract Task<CommandResult<TOutput>> HandleAsync(T command, CancellationToken cancellationToken = default);
    }
}