namespace Campoverde.QMS.Data.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.IsDeleted)
          .HasDefaultValue(false);

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(b => b.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(b => b.RoleId);

        builder.HasData(
          [
              new User
              {
                  Id = Guid.Parse("f220d2ea-fec4-4d80-8a86-ff1ba10d3acd"),
                  Email = "Admin",
                  Password = "ANznY7pOx7UkYvETjpkmbaKbhCxwZyUJhkpDg8QRajC/yruTQ1edEYQkHxfhdUFh",
                  Phone ="0123456789",
                  IsActive = true,
                  IsDeleted = false,
                  RoleId = Guid.Parse("98b22fa3-6666-41ad-b4d6-9726c7aa414a")
              }
              ]);
    }
}
