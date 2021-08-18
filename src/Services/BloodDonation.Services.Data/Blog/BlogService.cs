namespace BloodDonation.Services.Data.Blog
{
    using System.Collections.Generic;
    using System.Linq;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Mapping;

    public class BlogService : IBlogService
    {
        private readonly IDeletableEntityRepository<Blog> blogRepository;

        public BlogService(IDeletableEntityRepository<Blog> blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Blog> query = this.blogRepository.All().OrderBy(x => x.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        => this.blogRepository
                .All()
                .Where(x => x.Title == name)
               .To<T>().FirstOrDefault();
    }
}