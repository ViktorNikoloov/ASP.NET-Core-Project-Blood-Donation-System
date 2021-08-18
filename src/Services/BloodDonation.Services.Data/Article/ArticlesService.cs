namespace BloodDonation.Services.Data.Article
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Mapping;
    using BloodDonation.Web.ViewModels.Article;

    public class ArticlesService : IArticlesService
    {
        private readonly IDeletableEntityRepository<Blog> articlesRepository;

        public ArticlesService(IDeletableEntityRepository<Blog> articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Blog> query = this.articlesRepository.All().OrderBy(x => x.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        => this.articlesRepository
                .All()
                .Where(x => x.Title == name)
               .To<T>().FirstOrDefault();
    }
}