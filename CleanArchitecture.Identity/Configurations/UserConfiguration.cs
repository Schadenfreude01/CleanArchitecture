using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                    new ApplicationUser
                    {
                        Id = "1b3d6907-9755-4d4a-8b1e-a65b326a432c",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "admin@localhost.com",
                        Nombre = "Fabian",
                        Apellidos = "Monzon Ramirez",
                        UserName = "fmonzon",
                        NormalizedUserName = "fmonzon",
                        PasswordHash = hasher.HashPassword(null, "fmonzon2021"),
                        EmailConfirmed = true,
                    },
                    new ApplicationUser
                    {
                        Id = "fa694b6b-6fae-4c38-8535-80c6dbc74a76",
                        Email = "user@localhost.com",
                        NormalizedEmail = "user@localhost.com",
                        Nombre = "Juan",
                        Apellidos = "Perez",
                        UserName = "jperez",
                        NormalizedUserName = "jperez",
                        PasswordHash = hasher.HashPassword(null, "fmonzon2021"),
                        EmailConfirmed = true,
                    }
                );
        }
    }
}
