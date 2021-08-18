namespace BloodDonation.Services.Data.Article
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonation.Web.ViewModels.Blog;

    public interface IArticlesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        Task<int> CreateActicleAsync(ArticleCreateInputModel model, string userId);
    }
}
