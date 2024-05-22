namespace Campoverde.QMS.Data;

public class CampoverdeDbContext(DbContextOptions<CampoverdeDbContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Quote> Quote { get; set; }
    public DbSet<QuoteNote> QuoteNote { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Vehicle> Vehicle { get; set; }

    public override int SaveChanges()
    {
        SetUtcDateTimes();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetUtcDateTimes();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetUtcDateTimes()
    {
        foreach (var entry in ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.ClrType == typeof(DateTime) || property.Metadata.ClrType == typeof(DateTime?))
                {
                    var dateTimeValue = (DateTime?)property.CurrentValue;
                    if (dateTimeValue.HasValue && dateTimeValue.Value.Kind != DateTimeKind.Utc)
                    {
                        property.CurrentValue = DateTime.SpecifyKind(dateTimeValue.Value, DateTimeKind.Utc);
                    }
                }
            }
        }
    }
}

