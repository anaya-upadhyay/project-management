namespace ProjectManagement.Api.Queries.Core
{
    /// <summary>
    ///     Represents a Resultset which includes information about the Pagination
    /// </summary>
    /// <typeparam name="TResult">The Type of object returned with the Result</typeparam>
    public sealed class PaginatedResult<TResult> : IPaginatedResult<TResult>
    {
        public PaginatedResult(int page, int totalRecords, TResult[] items)
        {
            Page = page;
            TotalRecords = totalRecords;
            Items = items;
        }

        /// <inheritdoc />
        public int Page { get; }

        /// <inheritdoc />
        public int TotalRecords { get; }

        /// <inheritdoc />
        public TResult[] Items { get; }
    }
}