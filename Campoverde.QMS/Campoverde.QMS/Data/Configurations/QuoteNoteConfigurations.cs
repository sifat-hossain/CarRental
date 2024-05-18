using Campoverde.QMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campoverde.QMS.Data.Configurations;

public class QuoteNoteConfigurations : IEntityTypeConfiguration<QuoteNote>
{
    public void Configure(EntityTypeBuilder<QuoteNote> builder)
    {
        builder.ToTable(nameof(QuoteNote));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .UseIdentityColumn();

        builder.HasOne(b => b.Quote)
            .WithMany(q => q.Notes)
            .HasForeignKey(q => q.QuoteId);
    }
}
