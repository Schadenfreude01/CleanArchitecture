using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRolConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = "ec42e46e-0b45-427d-89f9-48924f85e95a",
                        UserId = "1b3d6907-9755-4d4a-8b1e-a65b326a432c"
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = "8b5bbe93-cb0b-4e11-8f57-2211347a0e45",
                        UserId = "fa694b6b-6fae-4c38-8535-80c6dbc74a76"
                    }
                );
        }
    }
}
