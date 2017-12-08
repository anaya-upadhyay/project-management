namespace ProjectManagement.Api.Handlers.Core
{
    /// <summary>
    /// A Validation Result returned by a Command Validation Handler that assert the validity of the command, before executing it
    /// </summary>
    public sealed class ValidationResult
    {
        /// <summary>
        /// Create a new Validation Result including the name of the field and the validation error
        /// </summary>
        /// <param name="name">The name of the Field issueing the validation error</param>
        /// <param name="reason">The reason of the validation error</param>
        public ValidationResult(string name, string reason)
        {
            this.Name = name;
            this.Reason = reason;
        }

        /// <summary>
        /// Returns the Name of the Field which caused the validation error
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Returns the reason of the validation error
        /// </summary>
        public string Reason { get; }
    }
}