using Campoverde.QMS.Common.Enums;

namespace Campoverde.QMS.ViewModel;

public class QuoteViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string SpanishAddress { get; set; }
    public int VehicleId { get; set; }
    public VehicleSizeEnum VehicleSize { get; set; }
    public VehicleTypeEnum VehicleType { get; set; }
}
