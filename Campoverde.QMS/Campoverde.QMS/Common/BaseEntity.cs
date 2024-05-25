namespace Campoverde.QMS.Common;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
