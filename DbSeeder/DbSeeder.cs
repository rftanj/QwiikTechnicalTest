using QwiikTechnicalTest.Models;
using QwiikTechnicalTest.Models.DB;
using System;

namespace QwiikTechnicalTest.DbSeeder
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationContext context)
        {
            if (!context.Users.Any(u => u.Email == "admin@gmail.com"))
            {
                context.Users.Add(new User
                {
                    Email = "admin@gmail.com",
                    PasswordHash = "PZ4gN4ftjkWBzP+jWoFbuN8cClWEuio9CmQqkYrjOfFWn5ywquVK/4f14a6+Gk4XgjL9u+tJ08piqgZPVApCIg==",
                    Salt = "X+zD57ydkOgdoJxr3/XTGQ==",
                    Role = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });

                await context.SaveChangesAsync();
            }
        }
    }

}
