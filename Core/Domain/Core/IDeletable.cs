namespace ProjectManagement.Domain.Core
{
    public interface IDeletable
    {
        bool IsDeleted { get; }

        void SetDeleted();
    }
}