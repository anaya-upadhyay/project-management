namespace ProjectManagement.Dal.Nhb
{
    public sealed class Transaction : ITransaction
    {
        private readonly NHibernate.ITransaction transaction;

        public Transaction(NHibernate.ITransaction transaction)
        {
            this.transaction = transaction;
        }

        public void Dispose()
        {
            transaction.Dispose();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }
    }
}