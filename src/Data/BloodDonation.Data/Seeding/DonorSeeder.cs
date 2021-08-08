namespace BloodDonation.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static BloodDonation.Common.GlobalConstants;

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
                        BloodType = BloodType.ZeroPositive,
                        DonationCount = 0,
                        Address = new Address
                        {
                            Town = new Town
                            {
                                Name = "Карлово",
                                PostCode = 4300,
                                Street = new Street
                                {
                                    Name = "Д-р Заменхов 4a",
                                },
                            },
                        },
                        ImageUrl = DefaulPicturetUrl, // Defaulf picture
                        PhoneNumber = user.PhoneNumber,
                    };

                    user.Donor = donor;
                }
            }
        }
    }
}
