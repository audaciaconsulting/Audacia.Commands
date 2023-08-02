using System;

namespace Audacia.Commands
{
    /// <summary>
    /// Exception when a command has no handler registered.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public class CommandWithNoHandlerException<TCommand> : Exception where TCommand : ICommand
    {
        /// <summary>
        /// Initializes an instance of <see cref="CommandWithNoHandlerException{TCommand}"/>.
        /// </summary>
        public CommandWithNoHandlerException()
            : base($"Cannot handle a command of type {typeof(TCommand).Name} as no handler is registered. You must register its handler using the CommandHandlingPipeline class.")
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="CommandWithNoHandlerException{TCommand}"/>.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public CommandWithNoHandlerException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="CommandWithNoHandlerException{TCommand}"/>.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CommandWithNoHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}