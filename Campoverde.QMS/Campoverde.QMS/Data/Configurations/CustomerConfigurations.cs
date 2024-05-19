using Campoverde.QMS.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campoverde.QMS.Data.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
           .UseIdentityColumn();
    }
}
