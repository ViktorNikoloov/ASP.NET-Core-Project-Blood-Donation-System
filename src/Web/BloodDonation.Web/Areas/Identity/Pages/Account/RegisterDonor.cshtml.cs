namespace BloodDonation.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Data.User;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    using static BloodDonation.Common.DataGlobalConstants.BasicUserInfoConstants;

    [AllowAnonymous]
    public class RegisterDonorModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IUsersService usersService;

        public RegisterDonorModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUsersService usersService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.usersService = usersService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [StringLength(100, ErrorMessage = "Полето \"{0}\" трябва да е между \"{2}\" и \"{1}\" символа.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърди паролата")]
            [Compare("Password", ErrorMessage = "\"Паролата\" и \"Потвърдената парола\" трябва да съвпадат")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Телефонен номер")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [RegularExpression(PhoneNumberRegex, ErrorMessage = "Тел. номер трябва да започва с \"+359\" или \"0\" и да съдържа общо \"10\" цифри.")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [Display(Name = "Вашата възраст между 18 и 65 години ли е ?")]
            public bool? Question1 { get; set; }

            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [Display(Name = "Вашето тегло над 50кг. ли е ?")]
            public bool? Question2 { get; set; }

            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [Display(Name = "Страдате ли от късогледство над 5 диоптъра ?")]
            public bool? Question3 { get; set; }

            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [Display(Name = "Имате ли сериозни заболявания ?")]
            public bool? Question4 { get; set; }

            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [Display(Name = "Страдате ли от тежки алергии ?")]
            public bool? Question5 { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("/~Donor/SuccessfullySentApplication");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = this.Input.Email, Email = this.Input.Email, PhoneNumber = this.Input.PhoneNumber, };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                         $"Моля потвърдете вашият имейл адрес.<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'></a>.");

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email });
                    }
                    else
                    {
                        await this.userManager.AddToRoleAsync(user, GlobalConstants.UnapprovedUserRoleName);

                        // TODO: make a question related table
                        Type clsType = typeof(InputModel);
                        PropertyInfo[] mInfo = clsType.GetProperties();

                        foreach (var property in mInfo)
                        {
                            var isDef = Attribute.IsDefined(property, typeof(DisplayAttribute));

                            if (isDef)
                            {
                                DisplayAttribute dispAttr =
                                 (DisplayAttribute)Attribute.GetCustomAttribute(
                                                    property, typeof(DisplayAttribute));

                                var propValue = this.Input.GetType().GetProperty(property.Name).GetValue(this.Input, null);

                                if (property.Name.StartsWith("Question"))
                                {
                                    await this.usersService.AddQuestionsAnswersToUser(new QuestionAnswer { Question = dispAttr.Name, Answer = propValue.ToString(), UserId = user.Id, User = user }, user);
                                }
                            }
                        }

                        this.TempData["Message"] = "Вашата заявка за \"Кръводарител\" се изпрати успешно, моля изчакайте одобрение от \"Администратор\"";
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.Page();
        }
    }
}
