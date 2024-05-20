using Campoverde.QMS.Enums;

namespace Campoverde.QMS.Models;

public class Quote : BaseEntity
{
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public VehicleSizeEnum VehicleSize { get; set; }
    public VehicleTypeEnum VehicleType { get; set; }
    public PassengerCountEnum PassengerCount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SpanishAddress { get; set; }
    public string SpecialRequet { get; set; }
    public decimal QuotePrice { get; set; }
    public DateTime LastUpdatedTime { get; set; }
    public DateTime LastUpdatedByUser { get; set; }
    public QuoteStatusEnum Status { get; set; }
    public List<QuoteNote> Notes { get; set; }
}
