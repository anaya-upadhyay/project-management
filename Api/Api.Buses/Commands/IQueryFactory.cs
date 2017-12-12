using ProjectManagement.Api.Queries.Core;
using ProjectManagement.Api.Queries.Handlers.Core;

namespace ProjectManagement.Api.Buses.Commands
{
    /// <summary>
    /// Resolve an available and registered Query Handler
    /// </summary>
    public interface IQueryFactory
    {
        /// <summary>
        /// Build a new Query Handler
        /// </summary>
        /// <typeparam name="TQuery">The Type of Query to be handled</typeparam>
        /// <typeparam name="TResult">The Type of TResult to be returned</typeparam>
        /// <returns>Returns a valid instance of a Query Handler</returns>
        IQueryHandler<TQuery, TResult> BuildHandler<TQuery, TResult>() where TQuery : IQuery<TResult>;
    }
}