using System;

namespace Audacia.Commands
{
    public static class PagingCommandExtensions
    {
        public static int GetSkip(this IPagingCommand command)
        {
            return command.PagingRequest.PageNumber * (command.PagingRequest.PageSize ?? int.MaxValue);
        }

        public static int GetTake(this IPagingCommand command)
        {
            return command.PagingRequest.PageSize ?? int.MaxValue;
        }

        public static int GetTotalPages(this IPagingCommand command, int totalRecords)
        {
            return Math.Max(
                (int)Math.Ceiling(totalRecords / (double)(command.PagingRequest.PageSize ?? int.MaxValue)), 1);
        }
    }
}
