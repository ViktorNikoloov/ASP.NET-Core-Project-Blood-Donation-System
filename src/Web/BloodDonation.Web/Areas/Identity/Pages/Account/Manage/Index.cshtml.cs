namespace BloodDonation.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.Cloudinary;
    using BloodDonation.Services.Data.Donor;
    using BloodDonation.Services.Data.Recipient;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using static BloodDonation.Common.DataGlobalConstants.AddressConstants;
    using static BloodDonation.Common.DataGlobalConstants.BasicUserInfoConstants;
    using static BloodDonation.Common.DataGlobalConstants.StreetConstants;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IRecipientsService recipientService;
        private readonly IDonorsService donorsService;
        private readonly ICloudinaryService cloudinaryService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRecipientsService recipientService,
            IDonorsService donorsService,
            ICloudinaryService cloudinaryService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.recipientService = recipientService;
            this.donorsService = donorsService;
            this.cloudinaryService = cloudinaryService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Телефонен номер")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Моля въведете име")]
            [RegularExpression("[А-я]+", ErrorMessage = "Името трябва да e на кирилица")]
            [Display(Name = "Име")]
            [StringLength(FirstNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = FirstNameMinLength)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Моля въведете презиме")]
            [RegularExpression("[А-я]+", ErrorMessage = "Презимето трябва да e на кирилица")]
            [Display(Name = "Презиме")]
            [StringLength(MiddleNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = MiddleNameMinLength)]
            public string MiddleName { get; set; }

            [Required(ErrorMessage = "Моля въведете фамилия")]
            [RegularExpression("[А-я]+", ErrorMessage = "Фамилията трябва да e на кирилица")]
            [Display(Name = "Фамилия")]
            [StringLength(LastNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = LastNameMinLength)]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Моля изберете профилна снимка")]
            [Display(Name = "Профилна снимка")]
            public string ImageUrl { get; set; }

            public IFormFile ImageFile { get; set; }

            [Display(Name = "Кръвна група")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            //[Range(1, 8)]
            public BloodType BloodType { get; set; }

            public int DonationCount { get; set; }

            [Display(Name = "Пол")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")] // The attribute is put on Enum type, only to set random error message.
            [EnumDataType(typeof(Gender))]
            //[Range(2, 3, ErrorMessage = "Полето \"{0}\" трябва да съдържа \"{1}\" или \"{2}\".")]
            public Gender Gender { get; set; }

            [Display(Name = "Град")]
            [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
            [StringLength(CityMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = CityMinLength)]
            public string CityName { get; set; }

            [Display(Name = "Пощенски код")]
            [Range(PostCodeMinValue, PostCodeMaxValue, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{1}\" и \"{2}\" символа.")]
            public int? PostCode { get; set; }

            [Display(Name = "Улица")]
            [StringLength(StreetNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = StreetNameMinLength)]
            public string StreetName { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            this.Username = userName;

            var isDonor = await this.userManager.IsInRoleAsync(user, GlobalConstants.DonorRoleName);
            var isRecipient = await this.userManager.IsInRoleAsync(user, GlobalConstants.RecipientRoleName);

            if (isDonor)
            {
                var donor = this.donorsService.GetDonorById(user.Id);

                this.Input = new InputModel
                {
                    FirstName = donor.FirstName,
                    MiddleName = donor.MiddleName,
                    LastName = donor.LastName,
                    CityName = donor.CityName,
                    StreetName = donor.StreetName,
                    PostCode = donor.PostCode,
                    Gender = donor.Gender,
                    BloodType = donor.BloodType,
                    PhoneNumber = phoneNumber,
                    ImageUrl = donor.ImageUrl,
                };

                return;
            }
            else if (isRecipient)
            {
                var recipient = this.recipientService.GetRecipientrById(user.Id);
                this.Input = new InputModel
                {
                    FirstName = recipient.FirstName,
                    MiddleName = recipient.MiddleName,
                    LastName = recipient.LastName,
                    CityName = recipient.CityName,
                    StreetName = recipient.StreetName,
                    PostCode = recipient.PostCode,
                    Gender = recipient.Gender,
                    BloodType = recipient.BloodType,
                    PhoneNumber = phoneNumber,
                    ImageUrl = recipient.ImageUrl,
                };

                return;
            }

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Нещо се случи докато се опитвахме да променим телефонния ви номер";
                    return this.RedirectToPage();
                }
            }

            var file = this.Input.ImageFile;

            var imageUrl = string.Empty;

            if (file != null)
            {
                if (file.Length > 0)
                {
                    var picturesFolderName = "Donors";
                    imageUrl = await this.cloudinaryService.UploadPictureAsync(file, picturesFolderName);
                }
            }
            else
            {
                imageUrl = this.Input.ImageUrl;
            }

            if (this.User.IsInRole(GlobalConstants.DonorRoleName))
            {
                var donor = this.donorsService.GetDonorById(user.Id);

                if (donor.FirstName == "Липсва" &&
                    donor.MiddleName == "Липсва" &&
                    donor.LastName == "Липсва" &&
                    donor.StreetName == "Липсва" &&
                    donor.Gender == Gender.Unknown &&
                    donor.BloodType == BloodType.Unknown)
                {
                await this.donorsService.FirstTimeDonorAddInfoAsync(user.Id, this.Input.FirstName, this.Input.MiddleName, this.Input.LastName, this.Input.CityName, this.Input.StreetName, this.Input.PostCode, this.Input.PhoneNumber, this.Input.Gender, this.Input.BloodType, imageUrl);
                }
                else
                {
                    await this.donorsService.UpdateSingInDonorInfoAsync(user.Id, this.Input.FirstName, this.Input.MiddleName, this.Input.LastName, this.Input.CityName, this.Input.StreetName, this.Input.PostCode, this.Input.PhoneNumber, imageUrl);
                }
            }
            else if (this.User.IsInRole(GlobalConstants.RecipientRoleName))
            {
                await this.recipientService.UpdateCurrentLoggedInRecipientInfoAsync(user.Id, this.Input.FirstName, this.Input.MiddleName, this.Input.LastName, this.Input.CityName, this.Input.StreetName, this.Input.PostCode, this.Input.PhoneNumber, imageUrl);
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Успешно запазихте промените";
            return this.RedirectToPage();
        }
    }
}
