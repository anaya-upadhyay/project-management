using ProjectManagement.Api.Queries.Core;
using ProjectManagement.Api.Queries.Results;

namespace ProjectManagement.Api.Queries
{
    /// <summary>
    /// Search Projects using a Text expression which spans over multiple information
    /// </summary>
    public sealed class SearchProjectsQuery : IQueryWithPagination<ProjectItemResult>, IQuery<IPaginatedResult<ProjectItemResult>>
    {
        /// <summary>
        /// Create a new instance of the Query and provides the required parameters
        /// </summary>
        /// <param name="text">The Text used to trigger the Search</param>
        /// <param name="page">The current page to be loaded</param>
        /// <param name="records">The amount of records to be loaded per page</param>
        public SearchProjectsQuery(string text, int page, int records)
        {
            Text = text;
            Page = page;
            Records = records;
        }

        /// <summary>
        /// The Text used in the SearchBox
        /// </summary>
        public string Text { get; }


        /// <inheritdoc />
        public int Page { get; }

        /// <inheritdoc />
        public int Records { get; }
    }
}