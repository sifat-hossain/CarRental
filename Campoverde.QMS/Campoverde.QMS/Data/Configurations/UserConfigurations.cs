using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campoverde.QMS.Data.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .UseIdentityColumn();

        builder.HasOne(b => b.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(b => b.RoleId);

        builder.HasData(
          [
              new User { Email = "Admin", Password = "ANznY7pOx7UkYvETjpkmbaKbhCxwZyUJhkpDg8QRajC/yruTQ1edEYQkHxfhdUFh", IsActive = true, IsDeleted = false, RoleId = 1 }
              ]);
    }
}
