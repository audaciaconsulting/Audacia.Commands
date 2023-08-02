using System;
using System.Threading;
using System.Threading.Tasks;

namespace Audacia.Commands.Decorators
{
    /// <summary>
    /// <see cref="ICommand"/> implementation to add the functionality to validate the incoming <see cref="ICommandHandler{T}"/> and stop the handling
    /// of the command if validation fails.
    /// Using this keeps the 'real' <see cref="ICommandHandler{T}"/> implementation clear of validation concerns.
    /// </summary>
    /// <typeparam name="T">The type of command.</typeparam>
    public class ValidatingDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandValidator<T> _validator;
        private readonly ICommandHandler<T> _wrappedHandler;

        /// <summary>
        /// Initializes an instance of <see cref="ValidatingDecorator{T}"/>.
        /// </summary>
        /// <param name="validator">The instance of <see cref="ICommandValidator{T}"/> that will validate the command.</param>
        /// <param name="wrappedHandler">The instance of <see cref="ICommandHandler{T}"/> that will handle the command.</param>
        /// <exception cref="ArgumentNullException"><paramref name="validator"/> is <see langword="null"/>.</exception>
        public ValidatingDecorator(ICommandValidator<T> validator, ICommandHandler<T> wrappedHandler)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _wrappedHandler = wrappedHandler;
        }

        /// <inheritdoc />
        public async Task<CommandResult> HandleAsync(T command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
            if (validationResult.IsSuccess)
            {
                return await _wrappedHandler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
            }

            return validationResult;
        }
    }

    /// <summary>
    /// <see cref="ICommandHandler{T,TOutput}"/> implementation to add the functionality to validate the incoming <see cref="ICommand"/> and stop the handling
    /// of the command if validation fails.
    /// Using this keeps the 'real' <see cref="ICommandHandler{T,TOutput}"/> implementation clear of validation concerns.
    /// </summary>
    /// <typeparam name="T">The type of command.</typeparam>
    /// <typeparam name="TOutput">The type of output.</typeparam>
    public class ValidatingDecorator<T, TOutput> : OutputCommandHandlerBase<T, TOutput> where T : ICommand
    {
        private readonly ICommandValidator<T> _validator;
        private readonly ICommandHandler<T, TOutput> _wrappedHandler;

        /// <summary>
        /// Initializes an instance of <see cref="ValidatingDecorator{T, TOutput}"/>.
        /// </summary>
        /// <param name="validator">The instance of <see cref="ICommandValidator{T}"/> that will validate the command.</param>
        /// <param name="wrappedHandler">The instance of <see cref="ICommandHandler{T, TOutput}"/> that will handle the command.</param>
        /// <exception cref="ArgumentNullException"><paramref name="validator"/> is <see langword="null"/>.</exception>
        public ValidatingDecorator(ICommandValidator<T> validator, ICommandHandler<T, TOutput> wrappedHandler)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _wrappedHandler = wrappedHandler;
        }

        /// <inheritdoc />
        public override async Task<CommandResult<TOutput>> HandleAsync(T command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
            if (validationResult.IsSuccess)
            {
                return await _wrappedHandler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
            }

            return CommandResult.FromExistingResult<TOutput>(validationResult);
        }
    }
}