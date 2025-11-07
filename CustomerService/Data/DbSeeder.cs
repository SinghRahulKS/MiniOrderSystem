using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using UserService.Repository.Entity;

namespace UserService.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(UserDbContext context)
        {
            await context.Database.MigrateAsync();
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Name = "Admin User",
                        Email = "admin@userservice.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        PhoneNo = "9876543210",
                        Role = "Admin"
                    },
                    new User
                    {
                        Name = "Rahul singh",
                        Email = "rahul@gmail.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Singh@123"),
                        PhoneNo = "1234567890",
                        Role = "User"
                    },
                    new User
                    {
                        Name = "Rahul Kumar",
                        Email = "rahulsingh@gmail.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Singh@123"),
                        PhoneNo = "9876512345",
                        Role = "User"
                    },
                    new User
                    {
                        Name = "Mike Ross",
                        Email = "mike.ross@example.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mike@123"),
                        PhoneNo = "9876523456",
                        Role = "User"
                    },
                    new User
                    {
                        Name = "Rachel Green",
                        Email = "rachel.green@example.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Rachel@123"),
                        PhoneNo = "9876534567",
                        Role = "User"
                    }
                };

                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }
        }
    }
}
