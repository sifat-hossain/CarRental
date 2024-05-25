namespace Campoverde.QMS.Data.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.IsDeleted)
          .HasDefaultValue(false);

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);
    }
}
