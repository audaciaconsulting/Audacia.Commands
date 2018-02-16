namespace Audacia.Commands
{
    /// <summary>
    /// A factory to obtain <see cref="ICommandHandler"/> instances to handle commands of a given type.
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Gets an <see cref="ICommandHandler{T}"/> instance to handle a command of type <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="executionMode"></param>
        /// <returns></returns>
        ICommandHandler<T> GetHandler<T>(CommandHandlingExecutionMode executionMode) where T : ICommand;

        /// <summary>
        /// Gets an <see cref="ICommandHandler{T,TOutput}"/> instance to handle a command of type <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="executionMode"></param>
        /// <returns></returns>
        ICommandHandler<T, TOutput> GetHandler<T, TOutput>(CommandHandlingExecutionMode executionMode) where T : ICommand;
    }
}