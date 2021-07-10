namespace BloodDonation.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(BlooddonationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
