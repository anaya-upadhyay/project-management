using Microsoft.EntityFrameworkCore.Storage;

namespace ProjectManagement.Dal.EfCore
{
    /// <summary>
    ///     Implementation of a Generic transaction contract using Entity Framework Core
    /// </summary>
    public sealed class Transaction : ITransaction
    {
        /// <summary>
        ///     The Unerlying Entity Framework Core transaction
        /// </summary>
        private readonly IDbContextTransaction transaction;

        /// <summary>
        ///     Create a new Generic Transaction by using an underlying Entity Framework Core transaction
        /// </summary>
        /// <param name="transaction">Return an instance of an ITransaction contract</param>
        public Transaction(IDbContextTransaction transaction)
        {
            this.transaction = transaction;
        }

        /// <summary>
        ///     Dispose the current transaction
        /// </summary>
        public void Dispose()
        {
            transaction.Dispose();
        }

        /// <summary>
        ///     Commit the current transaction
        /// </summary>
        public void Commit()
        {
            transaction.Commit();
        }

        /// <summary>
        ///     Rollback the current transaction
        /// </summary>
        public void Rollback()
        {
            transaction.Rollback();
        }
    }
}