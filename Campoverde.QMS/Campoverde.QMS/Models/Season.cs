namespace Campoverde.QMS.Models;

public class Season : BaseEntity
{
    public required string Name { get; set; }
    public List<VehiclePrice>? VehiclePrices { get; set; }
}
