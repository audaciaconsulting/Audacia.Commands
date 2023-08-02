using Audacia.Core;

namespace Audacia.Commands
{
    /// <summary>
    /// Represents a command that can be used for defining paging.
    /// </summary>
    /// <typeparam name="TPagingRequest">The type of paging request.</typeparam>
    public interface IPagingCommand<TPagingRequest> : ICommand
        where TPagingRequest : PagingRequest
    {
        /// <summary>
        /// Gets or sets the paging request.
        /// </summary>
        TPagingRequest PagingRequest { get; set; }
    }

    /// <summary>
    /// Represents a paging command that uses the default <see cref="PagingRequest"/>.
    /// </summary>
    public interface IPagingCommand : IPagingCommand<PagingRequest>
    {
    }

    /// <summary>
    /// Represents a paging command that uses the default <see cref="SortablePagingRequest"/>.
    /// </summary>
    public interface ISortablePagingCommand : IPagingCommand<SortablePagingRequest>
    {
    }
}
