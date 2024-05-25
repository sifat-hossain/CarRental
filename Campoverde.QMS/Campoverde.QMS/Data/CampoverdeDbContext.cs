namespace Campoverde.QMS.Data;

public class CampoverdeDbContext(DbContextOptions<CampoverdeDbContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Quote> Quote { get; set; }
    public DbSet<QuoteNote> QuoteNote { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Vehicle> Vehicle { get; set; }

    protected override void ConfigureConventions(
            ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<decimal>()
            .HavePrecision(9, 2);

        configurationBuilder
            .Properties<decimal?>()
            .HavePrecision(9, 2);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CampoverdeDbContext).Assembly);
    }
}

