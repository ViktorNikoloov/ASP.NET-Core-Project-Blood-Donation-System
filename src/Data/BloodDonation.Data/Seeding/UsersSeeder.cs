namespace BloodDonation.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        private readonly IConfiguration configuration;

        public UsersSeeder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SeedAsync(BloodDonationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string adminEmail = this.configuration["Root:AdminEmail"];
            string adminPassword = this.configuration["Root:AdminPassword"];

            await SeedUserAsync(userManager, adminEmail, adminPassword);
            await SeedUserAsync(userManager, "donor@abv.bg");
            await SeedUserAsync(userManager, "recipient@abv.bg");
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string username, string adminPassword = null)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                var appUser = new ApplicationUser();
                appUser.UserName = username;
                appUser.Email = username;
                appUser.PhoneNumber = "088888888";

                IdentityResult result = new IdentityResult();

                if (username == "donor@abv.bg")
                {
                    result = userManager.CreateAsync(appUser, "123456").Result;
                    appUser.PhoneNumber = "0888888888";
                }
                else if (username == "recipient@abv.bg")
                {
                    result = userManager.CreateAsync(appUser, "123456").Result;
                    appUser.PhoneNumber = "0889999999";
                }
                else
                {
                    result = userManager.CreateAsync(appUser, adminPassword).Result;
                    appUser.PhoneNumber = "0887777777";
                }

                if (result.Succeeded)
                {
                    if (username == "donor@abv.bg")
                    {
                        userManager.AddToRoleAsync(appUser, GlobalConstants.DonorRoleName).Wait();
                    }
                    else if (username == "recipient@abv.bg")
                    {
                        userManager.AddToRoleAsync(appUser, GlobalConstants.RecipientRoleName).Wait();
                    }
                    else
                    {
                        userManager.AddToRoleAsync(appUser, GlobalConstants.AdministratorRoleName).Wait();
                    }
                }
            }
        }
    }
}
