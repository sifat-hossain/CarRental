namespace Campoverde.QMS.Models;

public class Vehicle : BaseEntity
{
    public string Model { get; set; }
    public decimal Price { get; set; }
    public List<Quote> Quotes { get; set; }
}
