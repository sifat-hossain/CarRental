namespace Campoverde.QMS.Models;

public class Vehicle : BaseEntity
{
    public required string Model { get; set; }
    public VehicleSizeEnum VehicleSize { get; set; }
    public VehicleTypeEnum VehicleTypeEnum { get; set; }
    public string? PhotoUrl { get; set; }
    [NotMapped]
    public IFormFile? Image { get; set; }
    public List<Quote>? Quotes { get; set; }
    public List<VehiclePrice>? VehiclePrices { get; set; }

}
