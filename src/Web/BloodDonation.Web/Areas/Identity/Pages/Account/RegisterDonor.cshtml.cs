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
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using static BloodDonation.Common.DataGlobalConstants.AddressConstants;
    using static BloodDonation.Common.DataGlobalConstants.BasicUserInfoConstants;
    using static BloodDonation.Common.DataGlobalConstants.StreetConstants;

    [AllowAnonymous]
    public class RegisterDonorModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration configuration;

        public RegisterDonorModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [EmailAddress]
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
            [MinLength(FirstNameMinLength)]
            [MaxLength(FirstNameMaxLength)]
            [StringLength(FirstNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = FirstNameMinLength)]
            public string FirstName { get; set; }

            [Display(Name = "Бащино име")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [MinLength(MiddleNameMinLength)]
            [MaxLength(MiddleNameMaxLength)]
            [StringLength(MiddleNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = MiddleNameMinLength)]
            public string MiddleName { get; set; }

            [Display(Name = "Фамилия")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [MinLength(LastNameMinLength)]
            [MaxLength(LastNameMaxLength)]
            [StringLength(LastNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = LastNameMinLength)]
            public string LastName { get; set; }

            [Display(Name = "Url адрес")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [Url(ErrorMessage = "Невалиден \"Url\" адрес")]
            public string ImageUrl { get; set; }

            [Display(Name = "Прикачете профилна снимка")]
            public IFormFile ImageFile { get; set; }

            [Display(Name = "Пол")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")] // The attribute is put on Enum type, only to set random error message.
            [EnumDataType(typeof(Gender))]
            [Range(1, 2, ErrorMessage = "Полето \"{0}\" трябва да съдържа \"{1}\" или \"{2}\".")]
            public Gender Gender { get; set; }

            [Display(Name = "Град")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [MinLength(CityMinLength)]
            [MaxLength(CityMaxLength)]
            [StringLength(CityMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = CityMinLength)]
            public string CityName { get; set; }

            [Display(Name = "Пощенски код")]
            [Range(PostCodeMinValue, PostCodeMaxValue, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{1}\" и \"{2}\" символа.")]
            public int? PostCode { get; set; }

            [Display(Name = "Улица")]
            [MinLength(StreetNameMinLength)]
            [MaxLength(StreetNameMaxLength)]
            [StringLength(StreetNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = StreetNameMinLength)]
            public string StreetName { get; set; }

            [Display(Name = "Телефонен номер")]
            [RegularExpression(PhoneNumberRegex, ErrorMessage = "Тел. номер трябва да започва с \"+359\" или \"0\" и да съдържа общо \"10\" цифри.")]
            public string PhoneNumber { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Dogsitter/SuccessfullySentApplication");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, PhoneNumber = Input.PhoneNumber };

                var result = await _userManager.CreateAsync(user, Input.Password);

                var cloudinaryAccount = this.configuration.GetSection("Cloudinary");

                Account account = new Account(
                    this.configuration["Cloudinary:CloudName"],
                    this.configuration["Cloudinary:APIKey"],
                    this.configuration["Cloudinary:APISecret"]);

                Cloudinary cloudinary = new Cloudinary(account);

                var file = Input.ImageFile;

                var uploadResult = new ImageUploadResult();

                var imageUrl = "";

                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            var uploadParams = new ImageUploadParams()
                            {
                                File = new FileDescription(file.Name, stream),
                                Transformation = new Transformation().Width(100).Height(100).Gravity("face").Radius("max").Border("2px_solid_white").Crop("thumb"),
                            };

                            uploadResult = cloudinary.Upload(uploadParams);
                        }
                    }

                    imageUrl = uploadResult.Uri.ToString();
                }
                else
                {
                    imageUrl = Input.ImageUrl;
                }


                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"���� ���������� ���� ������ �� ��� <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'></a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await this._userManager.AddToRoleAsync(user, GlobalConstants.DonorRoleName);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        //await this.donorService.CreateOwnerAsync(user, Input.Address, Input.FirstName, Input.MiddleName, Input.LastName, Input.Gender, imageUrl, Input.PhoneNumber, user.Id, Input.Description);

                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
