namespace BloodDonation.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Web.Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<DeletePersonalDataModel> logger;
        private readonly IDeletableEntityRepository<Recipient> recipientManager;
        private readonly IDeletableEntityRepository<Donor> donorManager;

        public DeletePersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IDeletableEntityRepository<Recipient> recipientManager,
            IDeletableEntityRepository<Donor> donorManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.recipientManager = recipientManager;
            this.donorManager = donorManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            RequirePassword = await this.userManager.HasPasswordAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.RequirePassword = await this.userManager.HasPasswordAsync(user);
            if (this.RequirePassword)
            {
                if (!await this.userManager.CheckPasswordAsync(user, this.Input.Password))
                {
                    this.ModelState.AddModelError(string.Empty, "Грешна парола.");
                    return Page();
                }
            }

            var userId = await this.userManager.GetUserIdAsync(user);
            var userRole = await this.userManager.GetRolesAsync(user);
            var role = await this.userManager.RemoveFromRolesAsync(user, userRole);

            if (userRole[0] == "Recipient")
            {
                var currRecipient = this.recipientManager.All().FirstOrDefault(x => x.UserId == userId);
                this.recipientManager.Delete(currRecipient);
                await this.recipientManager.SaveChangesAsync();
            }
            else if (userRole[0] == "Donor")
            {
                var currDonor = this.donorManager.All().FirstOrDefault(x => x.UserId == userId);
                this.donorManager.Delete(currDonor);
                await this.donorManager.SaveChangesAsync();
            }

            var result = await this.userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await this.signInManager.SignOutAsync();

            this.logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return this.Redirect("~/");
        }
    }
}
