namespace Campoverde.QMS.Data.Configurations;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.IsDeleted)
          .HasDefaultValue(false);

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.HasData(
         [
             new Role
             {
                 Id = Guid.Parse("98b22fa3-6666-41ad-b4d6-9726c7aa414a"),
                 Name = "Admin",
                 IsActive = true,
                 IsDeleted = false
             },
             new Role
             {
                 Id = Guid.Parse("722901c9-31f4-486a-ae1c-058d6da261da"),
                 Name = "User",
                 IsActive = true,
                 IsDeleted = false
             }
             ]);
    }
}
