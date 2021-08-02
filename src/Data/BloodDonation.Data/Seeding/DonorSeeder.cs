namespace BloodDonation.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class DonorSeeder : ISeeder
    {
        public async Task SeedAsync(BloodDonationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedDonorAsync(userManager, "donor@abv.bg");
        }

        private static async Task SeedDonorAsync(UserManager<ApplicationUser> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                if (user.Donor == null)
                {
                    var donor = new Donor
                    {
                        UserId = user.Id,
                        FirstName = "Иван",
                        MiddleName = "Тодоров",
                        LastName = "Лудов",
                        Gender = Gender.Male,
                        Address = new Address
                        {
                            Town = new Town
                            {
                                Name = "Карлово",
                                PostCode = 4300,
                                Street = new Street
                                {
                                    Name = "Д-р Заменхов",
                                    Number = 4,
                                },
                            },
                        },
                        ImageUrl = "https://res.cloudinary.com/dvvbab0fs/image/upload/v1627247340/faoqwxe5cyxcadm0moks.jpg", // Defaulf picture
                        PhoneNumber = user.PhoneNumber,
                        WageRate = 10,
                    };

                    user.Donor = donor;
                }
            }
        }
    }
}
