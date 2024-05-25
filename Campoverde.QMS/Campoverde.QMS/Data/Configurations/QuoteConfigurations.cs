using Campoverde.QMS.Common.Constants;

namespace Campoverde.QMS.Data.Configurations;

public class QuoteConfigurations : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.ToTable(nameof(Quote));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.IsDeleted)
          .HasDefaultValue(false);

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.Property(b => b.QuotePrice)
            .HasColumnType(Constants.Precision.Decimal);

        builder.HasOne(c => c.Customer)
            .WithMany(q => q.Quotes)
            .HasForeignKey(c => c.CustomerId);

        builder.HasOne(v => v.Vehicle)
            .WithMany(q => q.Quotes)
            .HasForeignKey(v => v.VehicleId);
    }
}
