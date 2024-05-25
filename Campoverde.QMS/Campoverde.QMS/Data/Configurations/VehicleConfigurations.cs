using Campoverde.QMS.Common.Constants;

namespace Campoverde.QMS.Data.Configurations;

public class VehicleConfigurations : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable(nameof(Vehicle));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.Property(c => c.Price)
            .HasColumnType(Constants.Precision.Decimal);
        builder.HasData([
            new Vehicle
            {
                Id=Guid.Parse("056ac989-5eb4-4f07-8630-069098584cfe"),
                IsActive=true,
                IsDeleted=false,
                Model="BMW Luxery",
                Price=500
            }
            ]);
    }
}
