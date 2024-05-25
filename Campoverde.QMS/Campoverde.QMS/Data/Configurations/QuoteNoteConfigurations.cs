namespace Campoverde.QMS.Data.Configurations;

public class QuoteNoteConfigurations : IEntityTypeConfiguration<QuoteNote>
{
    public void Configure(EntityTypeBuilder<QuoteNote> builder)
    {
        builder.ToTable(nameof(QuoteNote));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.IsDeleted)
          .HasDefaultValue(false);

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(b => b.Quote)
            .WithMany(q => q.Notes)
            .HasForeignKey(q => q.QuoteId);
    }
}
