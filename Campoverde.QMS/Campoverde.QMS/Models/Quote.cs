namespace Campoverde.QMS.Models;

public class Quote : BaseEntity
{
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public VehicleSizeEnum VehicleSize { get; set; }
    public VehicleTypeEnum VehicleType { get; set; }
    public PassengerCountEnum PassengerCount { get; set; }

    [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyyy : htt}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }

    [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyyy : htt}", ApplyFormatInEditMode = true)]
    public DateTime EndDate { get; set; }
    public required string SpanishAddress { get; set; }
    public required string SpecialRequet { get; set; }
    public decimal QuotePrice { get; set; }
    public DateTime LastUpdatedTime { get; set; }
    public DateTime LastUpdatedByUser { get; set; }
    public QuoteStatusEnum Status { get; set; }

    [NotMapped]
    public string IsUserAccountNeeded { get; set; }
    public List<QuoteNote>? Notes { get; set; }
}
