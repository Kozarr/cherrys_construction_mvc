using cherrys_construction_mvc.Data;
using cherrys_construction_mvc.Models;
using cherrys_construction_mvc.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace cherrys_construction_mvc.DbInitializer
{

    // This is used to seed our database in Cloud once we upload project
    // Creation of admin accounts is blocked off, so we will seed the admin account into new database

    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            ILogger<DbInitializer> logger)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public void Initialize()
        {
            // apply migrations if they are not applied
            try
            {
                //if this is true - we have some migrations that have not been applied to the database
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {             
                throw;
            }
            // create roles if they are none
            // Creating Roles Here
            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Employee)).GetAwaiter().GetResult();

                // if role are not created, then create admin user


                // 1.Creating The Admin User
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@web.com",
                    Email = "admin@web.com",
                    Name = "Admin Account",
                    PhoneNumber = "1234567891",
                    StreetAddress = "321 Admin St",
                    PostalCode = "12345",
                    City = "City",
                    State = "TS",
                    EmailConfirmed = true,
                }, "Admin789*").GetAwaiter().GetResult();

                // 2. Finding the user in Database
                ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@web.com");

                if (user != null)
                {
                    // 3. Assiging the Admin Role - to the new Admin Account
                    _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();
                }
                else
                {
                    _logger.LogInformation("Could not find initialize user to give role privileges");
                }
               
            }
            return;
        }
    }
}
