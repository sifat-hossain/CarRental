namespace Campoverde.QMS.Models;

public class QuoteNote : BaseEntity
{
    public Guid QuoteId { get; set; }
    public Quote? Quote { get; set; }
    public string Notes { get; set; }
}
