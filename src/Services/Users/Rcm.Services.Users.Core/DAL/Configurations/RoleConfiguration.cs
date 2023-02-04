using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rcm.Services.Users.Core.Entities;

namespace Rcm.Services.Users.Core.DAL.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.Permissions)
            .WithMany(x => x.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "RolePermissions",
                x => x
                    .HasOne<Permission>()
                    .WithMany()
                    .HasForeignKey("PermissionId"),
                x => x
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId"),
                x => x.Property("RoleId").HasColumnOrder(0));
    }
}
