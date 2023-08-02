namespace Audacia.Commands
{
    /// <summary>
    /// A factory to obtain <see cref="ICommandHandler"/> instances to handle commands of a given type.
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Gets an <see cref="ICommandHandler{T}"/> instance to handle a command of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the command.</typeparam>
        /// <param name="executionMode">The <see cref="CommandHandlingExecutionMode"/> that defines how the pipeline should be executed.</param>
        /// <returns>An instance of <see cref="ICommandHandler{T}"/> that can handle a command of type <typeparamref name="T"/>.</returns>
        ICommandHandler<T> GetHandler<T>(CommandHandlingExecutionMode executionMode) where T : ICommand;

        /// <summary>
        /// Gets an <see cref="ICommandHandler{T,TOutput}"/> instance to handle a command of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the command.</typeparam>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="executionMode">The <see cref="CommandHandlingExecutionMode"/> that defines how the pipeline should be executed.</param>
        /// <returns>An instance of <see cref="ICommandHandler{T, TOutput}"/> that can handle a command of type <typeparamref name="T"/>.</returns>
        ICommandHandler<T, TOutput> GetHandler<T, TOutput>(CommandHandlingExecutionMode executionMode) where T : ICommand;
    }
}