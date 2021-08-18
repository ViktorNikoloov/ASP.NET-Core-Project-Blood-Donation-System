namespace BloodDonation.Web.Controllers
{
    using BloodDonation.Services.Data.Article;
    using BloodDonation.Web.ViewModels.Blog;

    using BloodDonation.Common;
    using BloodDonation.Web.Infrastructure;

    using Microsoft.AspNetCore.Authorization;
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

        public IActionResult ById()
        {
            return this.View();
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.articlesService.GetByName<ArticleByName>(name);
            if (viewModel == null)
            {
                return this.Redirect("/Home/StatusCodeError");
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Authorize]
        public IActionResult CreateArticle()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public IActionResult CreateArticle(ArticleCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var articleId = this.articlesService.CreateActicleAsync(input, this.User.GetId());

            return this.RedirectToAction(nameof(this.ById), new { id = articleId });
        }
    }
}
