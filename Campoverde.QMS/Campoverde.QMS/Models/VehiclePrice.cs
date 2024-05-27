namespace Campoverde.QMS.Models;

public class VehiclePrice : BaseEntity
{
    public required Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public required Guid SeasonId { get; set; }
    public Season? Season { get; set; }
    public required decimal Price { get; set; }
}
