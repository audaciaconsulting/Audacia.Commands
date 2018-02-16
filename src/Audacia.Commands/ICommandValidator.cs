using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands
{
    /// <summary>
    /// Exposes the functionality to validate a given <see cref="ICommand"/> object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommandValidator<in T> where T : ICommand
    {
        /// <summary>
        /// Validates the given <paramref name="command"/>.
        /// Returns a <see cref="CommandResult"/> representing success if the <paramref name="command"/> passes validation.
        /// Returns a <see cref="CommandResult"/> representing failure, together with a relevant error message, if the <paramref name="command"/> fails validation.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CommandResult> ValidateAsync(T command, CancellationToken cancellationToken = default(CancellationToken));
    }
}