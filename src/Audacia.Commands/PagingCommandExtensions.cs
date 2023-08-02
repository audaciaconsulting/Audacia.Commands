using System;
using Audacia.CodeAnalysis.Analyzers.Helpers.ParameterCount;
using Audacia.Core;

namespace Audacia.Commands
{
    /// <summary>
    /// Extensions to <see cref="IPagingCommand"/>.
    /// </summary>
    public static class PagingCommandExtensions
    {
        private const int MaxPageSize = int.MaxValue;
        private const int MinPageNumber = 0;

        /// <summary>
        /// Gets the number of items to skip.
        /// </summary>
        /// <typeparam name="T">The type of object being paged.</typeparam>
        /// <param name="command">The <see cref="IPagingCommand"/> object.</param>
        /// <returns>The number of items to skip.</returns>
        public static int GetSkip<T>(this IPagingCommand<T> command) where T : PagingRequest
        {
            if (command == null)
            {
                return 0;
            }

            return command.PagingRequest.PageNumber * (command.PagingRequest.PageSize ?? int.MaxValue);
        }

        /// <summary>
        /// Gets the number of items to take.
        /// </summary>
        /// <typeparam name="T">The type of object being paged.</typeparam>
        /// <param name="command">The <see cref="IPagingCommand"/> object.</param>
        /// <returns>The number of items to take.</returns>
        public static int GetTake<T>(this IPagingCommand<T> command) where T : PagingRequest
        {
            if (command == null)
            {
                return 0;
            }

            return command.PagingRequest.PageSize ?? int.MaxValue;
        }

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        /// <typeparam name="T">The type of object being paged.</typeparam>
        /// <param name="command">The <see cref="IPagingCommand"/> object.</param>
        /// <param name="totalRecords">The total number of records.</param>
        /// <returns>The total number of pages.</returns>
        public static int GetTotalPages<T>(this IPagingCommand<T> command, int totalRecords) where T : PagingRequest
        {
            if (command == null)
            {
                return 0;
            }

            return Math.Max(
                (int)Math.Ceiling(totalRecords / (double)(command.PagingRequest.PageSize ?? int.MaxValue)), 1);
        }

        /// <summary>
        /// Gets the direction of sorting.
        /// </summary>
        /// <typeparam name="T">The type of object being paged.</typeparam>
        /// <param name="command">The <see cref="IPagingCommand"/> object.</param>
        /// <returns>The direction of sorting as a SQL statement (i.e. "DESC" or "ASC").</returns>
        public static string GetSqlSortDirection<T>(this IPagingCommand<T> command) where T : SortablePagingRequest
        {
            return command?.PagingRequest.Descending == true ? "DESC" : "ASC";
        }

        /// <summary>
        /// Ensures that default paging is applied.
        /// </summary>
        /// <param name="command">The command containing the paging request.</param>
        /// <param name="pageSize">The page size to use.</param>
        /// <param name="pageNumber">The page number to use.</param>
        /// <returns>A <see cref="PagingRequest"/> object.</returns>
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
        /// <returns>A <see cref="SortablePagingRequest"/> object.</returns>
        [MaxParameterCount(5, Justification = "Needs this many parameters.")]
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
