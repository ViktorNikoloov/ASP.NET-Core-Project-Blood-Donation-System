namespace BloodDonation.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static BloodDonation.Common.GlobalConstants;

    public class RecipientsSeeder : ISeeder
    {
        public async Task SeedAsync(BloodDonationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRecipientAsync(userManager, "recipient@abv.bg");
        }

        private static async Task SeedRecipientAsync(UserManager<ApplicationUser> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                if (user.Recipient == null)
                {
                    var recipient = new Recipient
                    {
                        UserId = user.Id,
                        FirstName = "Виктор",
                        MiddleName = "Георгиев",
                        LastName = "Николов",
                        Gender = Gender.Male,
                        BloodType = BloodType.BPositive,
                        Address = new Address
                        {
                            Town = new Town
                            {
                                Name = "Пловдив",
                                PostCode = 4000,
                                Street = new Street
                                {
                                    Name = "Горно Броди 27",
                                },
                            },
                        },
                        ImageUrl = DefaulPicturetUrl, // Defaulf picture
                        PhoneNumber = user.PhoneNumber,
                    };

                    user.Recipient = recipient;
                }
            }
        }
    }
}
