using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands
{
    /// <summary>
    /// Exposes the functionality to validate a given <see cref="ICommand"/> object.
    /// </summary>
    /// <typeparam name="T">The type of the command.</typeparam>
    public interface ICommandValidator<in T> where T : ICommand
    {
        /// <summary>
        /// Validates the given <paramref name="command"/>.
        /// Returns a <see cref="CommandResult"/> representing success if the <paramref name="command"/> passes validation.
        /// Returns a <see cref="CommandResult"/> representing failure, together with a relevant error message, if the <paramref name="command"/> fails validation.
        /// </summary>
        /// <param name="command">The command to be validated.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> instance.</param>
        /// <returns>A <see cref="CommandResult"/> object representing the result of validation.</returns>
        Task<CommandResult> ValidateAsync(T command, CancellationToken cancellationToken = default);
    }
}