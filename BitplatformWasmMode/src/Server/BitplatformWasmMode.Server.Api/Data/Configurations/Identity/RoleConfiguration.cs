using BitplatformWasmMode.Server.Api.Models.Identity;

namespace BitplatformWasmMode.Server.Api.Data.Configurations.Identity;

public partial class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(role => role.Name).HasMaxLength(50);

    }
}

