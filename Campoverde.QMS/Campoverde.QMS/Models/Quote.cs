using System.ComponentModel.DataAnnotations;

namespace Campoverde.QMS.Models;

public class Quote : BaseEntity
{
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public VehicleSizeEnum VehicleSize { get; set; }
    public VehicleTypeEnum VehicleType { get; set; }
    public PassengerCountEnum PassengerCount { get; set; }

    [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }

    [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime EndDate { get; set; }
    public string SpanishAddress { get; set; }
    public string SpecialRequet { get; set; }
    public decimal QuotePrice { get; set; }
    public DateTime LastUpdatedTime { get; set; }
    public DateTime LastUpdatedByUser { get; set; }
    public QuoteStatusEnum Status { get; set; }

    [NotMapped]
    public string IsUserAccountNeeded { get; set; }
    public List<QuoteNote>? Notes { get; set; }
}
