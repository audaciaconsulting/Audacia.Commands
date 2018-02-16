using System;

namespace Audacia.Commands
{
    public class CommandWithNoHandlerException<TCommand> : Exception where TCommand : ICommand
    {
        public CommandWithNoHandlerException()
            : base($"Cannot handle a command of type {typeof(TCommand).Name} as no handler is registered. You must register its handler using the CommandHandlingPipeline class.")
        {
            
        }
    }
}