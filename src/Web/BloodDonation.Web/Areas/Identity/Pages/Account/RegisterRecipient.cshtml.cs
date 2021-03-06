namespace BloodDonation.Web.Areas.Identity.Pages.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.Cloudinary;
    using BloodDonation.Services.Data.Email;
    using BloodDonation.Services.Data.Recipient;
    using BloodDonation.Services.Messaging;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    using static BloodDonation.Common.DataGlobalConstants.AddressConstants;
    using static BloodDonation.Common.DataGlobalConstants.BasicUserInfoConstants;
    using static BloodDonation.Common.DataGlobalConstants.StreetConstants;
    using static BloodDonation.Common.GlobalConstants;

    [AllowAnonymous]
    public class RegisterRecipientModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IEmailsService emailsService;
        private readonly IRecipientsService recipientsService;
        private readonly ICloudinaryService cloudinaryService;

        public RegisterRecipientModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IEmailsService emailsService,
            IRecipientsService recipientsService,
            ICloudinaryService cloudinaryService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.emailsService = emailsService;
            this.recipientsService = recipientsService;
            this.cloudinaryService = cloudinaryService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
            [Display(Name = "Имейл")]
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

            [Display(Name = "Име")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [RegularExpression("[А-я]+", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
            [StringLength(FirstNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = FirstNameMinLength)]
            public string FirstName { get; set; }

            [Display(Name = "Презиме")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [RegularExpression("[А-я]+", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
            [StringLength(MiddleNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = MiddleNameMinLength)]
            public string MiddleName { get; set; }

            [Display(Name = "Фамилия")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [RegularExpression("[А-я]+", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
            [StringLength(LastNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = LastNameMinLength)]
            public string LastName { get; set; }

            [Display(Name = "Кръвна група")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [Range(1, 8)]
            public BloodType BloodType { get; set; }

            [Display(Name = "Url адрес")]
            //[Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [Url(ErrorMessage = "Невалиден \"Url\" адрес")]
            public string ImageUrl { get; set; }

            [Display(Name = "Прикачете профилна снимка")]
            public IFormFile ImageFile { get; set; }

            [Display(Name = "Пол")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")] // The attribute is put on Enum type, only to set random error message.
            [EnumDataType(typeof(Gender))]
            [Range(2, 3, ErrorMessage = "Полето \"{0}\" трябва да съдържа \"{1}\" или \"{2}\".")]
            public Gender Gender { get; set; }

            [Display(Name = "Град")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [RegularExpression("[А-я]+", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
            [StringLength(CityMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = CityMinLength)]
            public string CityName { get; set; }

            [Display(Name = "Пощенски код")]
            [Range(PostCodeMinValue, PostCodeMaxValue, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{1}\" и \"{2}\" символа.")]
            public int? PostCode { get; set; }

            [Display(Name = "Улица")]
            [RegularExpression(@"[А-я]+\s*[А-я]*\s*[0-9]*", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
            [StringLength(StreetNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = StreetNameMinLength)]
            public string StreetName { get; set; }

            [Display(Name = "Телефонен номер")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [RegularExpression(PhoneNumberRegex, ErrorMessage = "Тел. номер трябва да започва с \"+359\" или \"0\" и да съдържа общо \"10\" цифри.")]
            public string PhoneNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = this.Input.Email, Email = this.Input.Email, PhoneNumber = this.Input.PhoneNumber };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                var file = this.Input.ImageFile;

                var imageUrl = string.Empty;

                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        var picturesFolderName = "Recipients";

                        imageUrl = await this.cloudinaryService.UploadPictureAsync(file, picturesFolderName);
                    }
                }
                else
                {
                    if (this.Input.ImageUrl == null)
                    {
                        imageUrl = DefaulPicturetUrl; // default picture
                    }
                    else
                    {
                        imageUrl = this.Input.ImageUrl;
                    }
                }

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

                    //await this.emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
                    //    $"Моля потвърдете вашият имейл адрес.<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'></a>.");

                    this.TempData["Message"] = "Вашият акаунт беше създаден успешно.";

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email });
                    }
                    else
                    {
                        await this.userManager.AddToRoleAsync(user, GlobalConstants.RecipientRoleName);
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        await this.recipientsService.CreateRecipientAsync(user, user.Id, this.Input.FirstName, this.Input.MiddleName, this.Input.LastName, this.Input.CityName, this.Input.StreetName, this.Input.PostCode, this.Input.Gender, this.Input.BloodType, imageUrl, this.Input.PhoneNumber);

                        var subject = "Нов регистриран реципиент";
                        var emailHtml = this.emailsService.GenerateEmailRecipientNewRegistration(user, subject);
                        await this.emailSender.SendEmailAsync(GlobalConstants.SystemEmail, GlobalConstants.SystemName, GlobalConstants.SystemEmail, subject, emailHtml);

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
