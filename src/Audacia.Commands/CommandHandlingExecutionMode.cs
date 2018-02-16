namespace Audacia.Commands
{
    /// <summary>
    /// Enum to specify in which context certain command handlers should run.
    /// Its specific use is generally to govern when a decorator should be executed.
    /// For example, the saving decorator will usually be run when a command is being executed from the command dispatcher (FullPipeline mode),
    /// but will usually NOT be run when a command is being executed from within another command handler.
    /// </summary>
    public enum CommandHandlingExecutionMode
    {
        /// <summary>
        /// Use this option when the handler(s) should always execute, regardless of whether they are being run from within another handler or from the command dispatcher.
        /// </summary>
        Core,

        /// <summary>
        /// Use this option when the handler(s) should only execute when being run as part of a full pipeline from the command dispatcher.
        /// </summary>
        FullPipelineOnly
    }
}