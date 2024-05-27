namespace Campoverde.QMS.Data.Configurations;

public class VehiclePriceConfiguration : IEntityTypeConfiguration<VehiclePrice>
{
    public void Configure(EntityTypeBuilder<VehiclePrice> builder)
    {
        builder.ToTable(nameof(VehiclePrice));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.IsDeleted)
          .HasDefaultValue(false);

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.Property(b => b.Price)
            .HasColumnType(Constants.Precision.Decimal);

        builder.HasOne(c => c.Vehicle)
            .WithMany(b => b.VehiclePrices)
            .HasForeignKey(c => c.VehicleId);

        builder.HasOne(c => c.Season)
           .WithMany(b => b.VehiclePrices)
           .HasForeignKey(c => c.SeasonId);
    }
}
