namespace BloodDonation.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

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
            if (user != null)
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
                        Address = new Address
                        {
                            Town = new Town
                            {
                                Name = "Пловдив",
                                PostCode = 4000,
                                Street = new Street
                                {
                                    Name = "Горно Броди",
                                    Number = 27,
                                },
                            },
                        },
                        ImageUrl = "https://res.cloudinary.com/dvvbab0fs/image/upload/v1627247340/faoqwxe5cyxcadm0moks.jpg", // Defaulf picture
                        PhoneNumber = user.PhoneNumber,
                    };

                    user.Recipient = recipient;
                }
            }
        }
    }
}
