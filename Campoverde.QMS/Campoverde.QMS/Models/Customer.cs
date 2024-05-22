
namespace Campoverde.QMS.Models;

public class Customer : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public AgeEnum Age { get; set; }
    public GenderEnum Gender { get; set; }
    public string? HomeAddress { get; set; }
    public string? Nationality { get; set; }
    public string? SpanishAddress { get; set; }
    public string? DriverName { get; set; }
    public string? PrimaryDrivingLicenceNumber { get; set; }
    public string? SecondaryDrivingLicenseNumber { get; set; }
    public string? LicenseExpiryDate { get; set; }
    public string? DriverPassportNumber { get; set; }
    public List<Quote>? Quotes { get; set; }
}
