namespace Campoverde.QMS.Models;

public class QuoteNote : BaseEntity
{
    public int QuoteId { get; set; }
    public Quote? Quote { get; set; }
    public string Notes { get; set; }
}
