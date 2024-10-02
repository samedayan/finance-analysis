using Finance.Analysis.Infrastructure.ValueObjects;

#pragma warning disable CS8618
namespace Finance.Analysis.Persistence.PostgresSql.Domain.Abstract;

public abstract class Entity : IEntity<Guid>, IAuditableEntity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        if (id.Equals(Guid.Empty)) throw new ArgumentException($"{nameof(id)} cannot be empty");

        Id = id;
    }

    public AuditInformation AuditInformation { get; set; } = new();
    public Guid Id { get; set; }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj.GetType().IsAssignableFrom(typeof(Entity)) && Equals((Entity)obj);
    }

    public bool Equals(Entity other)
    {
        return Id.Equals(other.Id);
    }

    public int CompareTo(Entity other)
    {
        return Id.CompareTo(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity a, Entity b)
    {
        return a.CompareTo(b) == 0;
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }
}