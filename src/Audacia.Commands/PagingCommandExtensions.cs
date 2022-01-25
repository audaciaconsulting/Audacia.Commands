using System;
using Audacia.Core;

namespace Audacia.Commands
{
    public static class PagingCommandExtensions
    {
        private const int MaxPageSize = int.MaxValue;
        private const int MinPageNumber = 0;

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

        /// <summary>
        /// Ensures that default paging is applied.
        /// </summary>
        /// <param name="command">The command containing the paging request.</param>
        /// <param name="pageSize">The page size to use.</param>
        /// <param name="pageNumber">The page number to use.</param>
        public static PagingRequest EnsureDefaultPaging(
            this IPagingCommand command,
            int pageSize = MaxPageSize,
            int pageNumber = MinPageNumber)
        {
            return command?.PagingRequest ?? new PagingRequest(pageSize, pageNumber);
        }

        /// <summary>
        /// Ensures that default paging is applied.
        /// </summary>
        /// <param name="command">The command containing the paging request.</param>
        /// <param name="columnName">The column on which to sort.</param>
        /// <param name="pageSize">The page size to use.</param>
        /// <param name="pageNumber">The page number to use.</param>
        /// <param name="descending">Whether or not sorting should be descending.</param>
        public static SortablePagingRequest EnsureDefaultSortablePaging(
            this ISortablePagingCommand command,
            string columnName,
            int pageSize = MaxPageSize,
            int pageNumber = MinPageNumber,
            bool descending = false)
        {
            return !string.IsNullOrWhiteSpace(command?.PagingRequest?.SortProperty)
                ? command.PagingRequest
                : new SortablePagingRequest(pageSize, pageNumber)
                {
                    SortProperty = columnName,
                    Descending = descending
                };
        }
    }
}
