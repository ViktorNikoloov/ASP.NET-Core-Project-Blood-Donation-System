namespace BloodDonation.Services.Data.Article
{
    using System.Collections.Generic;

    public interface IArticlesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
