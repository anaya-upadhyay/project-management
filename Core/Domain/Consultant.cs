namespace ProjectManagement.Domain
{
    public sealed class Consultant : Person
    {
        /// <summary>
        ///     For OR/M usage
        /// </summary>
        private Consultant()
        {
            
        }

        /// <inheritdoc />
        public Consultant(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }
    }
}