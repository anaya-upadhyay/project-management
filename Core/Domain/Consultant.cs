namespace ProjectManagement.Domain
{
    public sealed class Consultant : Person
    {
        /// <inheritdoc />
        public Consultant(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }
    }
}