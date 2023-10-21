using NewCall_Api.Models;
using NewCall_Api.Services;
using NewCall_Api.Helpers;

namespace NewCall_Api.Database
{
    public class DbInitializer
    {

        public static void Initialize(ApplicationDBContext dbContext)
        {
            if (!dbContext.Students.Any())
            {
                var defaultStudent = new Students
                (
                    1,
                   "John",
                   "Doe",
                   "FA"
                );

                dbContext.Students.Add(defaultStudent);
                dbContext.SaveChangesAsync();
            }

            if (!dbContext.Admins.Any())
            {
                var defaultAdmin = new Admins
                {
                    identifiant = "admin",
                    password = "password",
                };
                string hashedPassword = PasswordHasher.HashPassword(defaultAdmin.password);

                defaultAdmin.password = hashedPassword;

                dbContext.Admins.Add(defaultAdmin);
                dbContext.SaveChangesAsync();
            }
        }

    }
}
