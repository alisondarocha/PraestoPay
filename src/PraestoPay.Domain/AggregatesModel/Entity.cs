using UUIDNext;

namespace PraestoPay.Domain.AggregatesModel;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public bool IsDeleted { get; protected set; }

    protected Entity()
    {
        Id = Uuid.NewSequential();
    }

    public void CreateTimestamp()
    {
    }

    public void UpdateTimestamp()
    {
    }

    public void Remove()
    {
        IsDeleted = true;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}