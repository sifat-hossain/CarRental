namespace Campoverde.QMS.Models;

public class Vehicle : BaseEntity
{
    public required string Model { get; set; }
    public List<Quote>? Quotes { get; set; }
    public List<VehiclePrice>? VehiclePrices { get; set; }

}
