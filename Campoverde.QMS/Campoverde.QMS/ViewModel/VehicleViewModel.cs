namespace Campoverde.QMS.ViewModel
{
    public class VehicleViewModel
    {
        public Guid Id { get; set; }
        public required string Model { get; set; }
        public string VehicleSize { get; set; }
        public string VehicleType { get; set; }
        public string? PhotoUrl { get; set; }
        public decimal Price { get; set; }
    }
}
