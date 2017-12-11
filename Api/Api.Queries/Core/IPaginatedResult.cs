namespace ProjectManagement.Api.Queries.Core
{
    /// <summary>
    ///     Returns a result which is composed by a list of items and information about the pagination
    /// </summary>
    public interface IPaginatedResult<out TResult>
    {
        /// <summary>
        ///     The Page returned with the records set
        /// </summary>
        int Page { get; }

        /// <summary>
        ///     The Amount of Total Records available with the Provided Query
        /// </summary>
        int TotalRecords { get; }

        /// <summary>
        ///     An Array of items, returned by the Result
        /// </summary>
        TResult[] Items { get; }
    }
}