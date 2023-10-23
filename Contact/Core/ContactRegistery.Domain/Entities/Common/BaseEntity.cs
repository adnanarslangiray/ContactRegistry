namespace ContactRegistry.Domain.Entities.Common;

public class BaseEntity {
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; }
    public virtual DateTime UpdatedDate { get; set; }
}