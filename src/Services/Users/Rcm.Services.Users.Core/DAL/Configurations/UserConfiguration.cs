using Rcm.Services.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rcm.Services.Users.Core.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Password).IsRequired().HasMaxLength(500);

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                x => x 
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId"),
                x => x
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId"),
                x => x.Property("UserId").HasColumnOrder(0));
    }
}
