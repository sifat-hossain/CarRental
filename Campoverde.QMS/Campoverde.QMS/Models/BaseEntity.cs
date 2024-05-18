namespace Campoverde.QMS.Models;

public class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
