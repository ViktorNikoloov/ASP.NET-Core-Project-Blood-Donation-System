namespace BloodDonation.Web.Controllers
{
    using BloodDonation.Services.Data.Article;
    using BloodDonation.Web.ViewModels.Article;

    using Microsoft.AspNetCore.Mvc;

    public class BlogController : Controller
    {
        private readonly IArticlesService articlesService;

        public BlogController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IActionResult Index()
        {
            var viewModel = new ArticlesListViewModel
            {
                Articles = this.articlesService.GetAll<ArticleViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.articlesService.GetByName<ArticleByName>(name);
            return this.View(viewModel);
        }
    }
}
