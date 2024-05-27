namespace Campoverde.QMS.Data.Configurations;

public class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable(nameof(Season));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.IsDeleted)
          .HasDefaultValue(false);

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.Property(b => b.Name)
            .HasMaxLength(Constants.FieldSize.Name);

        builder.HasData([
            new Season{Id=Guid.Parse("6f475b1d-4563-4744-92f3-101421938d5d"), Name="Low Season"},
            new Season{Id=Guid.Parse("b94a7602-3646-43cd-a5a5-2965df0c6ab5"), Name="Mid Season"},
            new Season{Id=Guid.Parse("c19e488d-e4d1-4433-9651-55d72c3e9662"), Name="High Season"}
            ]);
    }
}
