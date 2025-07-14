namespace Domain.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; protected set; } = DateTime.UtcNow;
        public bool IsDeleted { get; protected set; } = false;

        public void Delete()
        {
            IsDeleted = true;
            UpdateModifiedDate();
        }

        public void UpdateModifiedDate()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
