namespace BloodDonation.Web.ViewModels.Blog
{
    using System.ComponentModel.DataAnnotations;

    public class ArticleCreateInputModel
    {
        public int ArticleId { get; set; }

        [Display(Name = "Име на статията")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [RegularExpression(@"[А-я]+.*\s*[А-я]*", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
        [StringLength(30, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = 5)]
        public string Title { get; set; }

        [Display(Name = "Описание на Статията")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        //[RegularExpression(@"[А-я]+.*\s*[А-я]*", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
        [StringLength(int.MaxValue, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = 100)]
        public string Description { get; set; }

        [Display(Name = "Url на заглавната снимка")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [Url(ErrorMessage = "Полето \"{0}\" не е валидно.")]
        public string ImageUrl { get; set; }
    }
}
