using Audacia.Core;

namespace Audacia.Commands
{
    public interface IPagingCommand<TPagingRequest> : ICommand
        where TPagingRequest : PagingRequest
    {
        TPagingRequest PagingRequest { get; set; }
    }

    public interface IPagingCommand : IPagingCommand<PagingRequest>
    {
    }

    public interface ISortablePagingCommand : IPagingCommand<SortablePagingRequest>
    {
    }
}
