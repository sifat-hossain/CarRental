using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campoverde.QMS.Data.Configurations;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();

        builder.HasData(
         [
             new Role { Name = "Admin", IsActive = true, IsDeleted = false }
             ]);
    }
}
