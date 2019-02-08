using System;
using Audacia.Core;

namespace Audacia.Commands
{
    public static class PagingCommandExtensions
    {
        public static int GetSkip<T>(this IPagingCommand<T> command) where T : PagingRequest
        {
            return command.PagingRequest.PageNumber * (command.PagingRequest.PageSize ?? int.MaxValue);
        }

        public static int GetTake<T>(this IPagingCommand<T> command) where T : PagingRequest
        {
            return command.PagingRequest.PageSize ?? int.MaxValue;
        }

        public static int GetTotalPages<T>(this IPagingCommand<T> command, int totalRecords) where T : PagingRequest
        {
            return Math.Max(
                (int)Math.Ceiling(totalRecords / (double)(command.PagingRequest.PageSize ?? int.MaxValue)), 1);
        }

        public static string GetSqlSortDirection<T>(this IPagingCommand<T> command) where T : SortablePagingRequest
        {
            return command.PagingRequest.Descending ? "DESC" : "ASC";
        }
    }
}
