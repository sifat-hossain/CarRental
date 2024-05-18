using Campoverde.QMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campoverde.QMS.Data.Configurations;

public class QuoteConfigurations : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.ToTable(nameof(Quote));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .UseIdentityColumn();

        builder.HasOne(c => c.Customer)
            .WithMany(q => q.Quotes)
            .HasForeignKey(c => c.CustomerId);

        builder.HasOne(v => v.Vehicle)
            .WithMany(q => q.Quotes)
            .HasForeignKey(v => v.VehicleId);
    }
}
