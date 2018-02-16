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
    /// <typeparam name="T"></typeparam>
    public class ValidatingDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandValidator<T> _validator;
        private readonly ICommandHandler<T> _wrappedHandler;

        public ValidatingDecorator(ICommandValidator<T> validator, ICommandHandler<T> wrappedHandler)
        {
            _validator = validator ?? throw new ArgumentNullException();
            _wrappedHandler = wrappedHandler;
        }

        public async Task<CommandResult> HandleAsync(T command, CancellationToken cancellationToken = new CancellationToken())
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsSuccess)
            {
                return await _wrappedHandler.HandleAsync(command, cancellationToken);
            }

            return validationResult;
        }
    }

    /// <summary>
    /// <see cref="ICommandHandler{T,TOutput}"/> implementation to add the functionality to validate the incoming <see cref="ICommand"/> and stop the handling
    /// of the command if validation fails.
    /// Using this keeps the 'real' <see cref="ICommandHandler{T,TOutput}"/> implementation clear of validation concerns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public class ValidatingDecorator<T, TOutput> : OutputCommandHandlerBase<T, TOutput> where T : ICommand
    {
        private readonly ICommandValidator<T> _validator;
        private readonly ICommandHandler<T, TOutput> _wrappedHandler;

        public ValidatingDecorator(ICommandValidator<T> validator, ICommandHandler<T, TOutput> wrappedHandler)
        {
            _validator = validator ?? throw new ArgumentNullException();
            _wrappedHandler = wrappedHandler;
        }

        public override async Task<CommandResult<TOutput>> HandleAsync(T command, CancellationToken cancellationToken = new CancellationToken())
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsSuccess)
            {
                return await _wrappedHandler.HandleAsync(command, cancellationToken);
            }

            return CommandResult.FromExistingResult<TOutput>(validationResult);
        }
    }
}