using System;

namespace ProjectManagement.Dal
{
    /// <summary>
    ///     Represents a Unit of Work Transaction
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        ///     Commit all changes available in the current Transaction
        /// </summary>
        void Commit();

        /// <summary>
        ///     Rollback all changes available in the current Transaction
        /// </summary>
        void Rollback();
    }
}