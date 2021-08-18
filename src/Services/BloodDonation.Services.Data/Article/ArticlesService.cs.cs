namespace BloodDonation.Services.Data.Article
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Mapping;
    using BloodDonation.Web.ViewModels.Blog;

    public class ArticlesService : IArticlesService
    {
        private readonly IDeletableEntityRepository<Article> articlesRepository;

        public ArticlesService(IDeletableEntityRepository<Article> articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Article> query = this.articlesRepository.All().OrderBy(x => x.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        => this.articlesRepository
                .All()
                .Where(x => x.Title == name.Replace('-', ' '))
               .To<T>().FirstOrDefault();

        public async Task<int> CreateActicleAsync(ArticleCreateInputModel input, string userId)
        {
            var article = new Article
            {
                Id = input.ArticleId,
                UserId = userId,
                Title = input.Title,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
            };

            await this.articlesRepository.AddAsync(article);
            await this.articlesRepository.SaveChangesAsync();

            return article.Id;
        }
    }
}