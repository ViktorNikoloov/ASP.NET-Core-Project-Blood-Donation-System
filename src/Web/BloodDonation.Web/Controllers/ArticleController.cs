namespace BloodDonation.Web.Controllers
{
    using BloodDonation.Services.Data.Article;
    using BloodDonation.Web.ViewModels.Article;

    using Microsoft.AspNetCore.Mvc;

    public class ArticleController : Controller
    {
        private readonly IArticlesService articlesService;

        public ArticleController(IArticlesService articlesService)
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

    }
}
