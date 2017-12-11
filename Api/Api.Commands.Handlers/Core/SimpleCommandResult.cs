namespace ProjectManagement.Api.Handlers.Core
{
    /// <summary>
    /// A Simple Command Execution result
    /// </summary>
    public sealed class SimpleCommandResult : ICommandResult
    {
        /// <summary>
        ///     Create a new Simple Command Result with a success status
        /// </summary>
        /// <param name="success">True if the Execution of the Command was successfull</param>
        /// <param name="reason">The reason of the failure</param>
        public SimpleCommandResult(bool success, string reason = "")
        {
            Reason = reason;
            Success = success;
        }

        /// <summary>
        ///     Return a boolean value indicating if the Command has been executed succesfully
        /// </summary>
        public bool Success { get; }

        /// <summary>
        ///     In case of failure contains the reason of the failure
        /// </summary>
        public string Reason { get; }
    }
}