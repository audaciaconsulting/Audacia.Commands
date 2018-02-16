namespace Audacia.Commands
{
    /// <summary>
    /// Interface to be implemented by all commands.
    /// </summary>
    public interface ICommand
    {
        // Just a marker interface at present but used for generic constraint purposes
        // May also be used in future if all commands have to implemented a particular property (e.g. an Id) for logging, etc.
    }
}