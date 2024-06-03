namespace Campoverde.QMS.ViewModel;

public class QuoteViewModel
{
    public string PickupLocation { get; set; }
    public string DropLocation { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Email { get; set; }
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public int Age { get; set; }
    public GenderEnum Gender { get; set; }
    public string? Nationality { get; set; }
    public string IsUserAccountNeeded { get; set; }
    public List<Vehicle> AvailableVehicles { get; set; }
}
