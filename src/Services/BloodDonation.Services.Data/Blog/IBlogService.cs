namespace BloodDonation.Services.Data.Blog
{
    using System.Collections.Generic;

    public interface IBlogService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
