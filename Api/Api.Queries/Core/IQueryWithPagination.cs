namespace ProjectManagement.Api.Queries.Core
{
    /// <summary>
    /// Specialized query which provides also information about the Pagination parameters
    /// </summary>
    public interface IQueryWithPagination<TResult> : IQuery<TResult>
    {
        /// <summary>
        /// The Current Page to be loaded
        /// </summary>
        /// <remarks>The Page count start from 1 and not from 0</remarks>
        int Page { get; }

        /// <summary>
        /// The amount of records to be loaded by the query for the selected page
        /// </summary>
        int Records { get; }
    }
}