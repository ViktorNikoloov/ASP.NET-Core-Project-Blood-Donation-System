namespace BloodDonation.Web.Controllers
{
    using System.Threading.Tasks;

    using BloodDonation.Data.Models;
    using BloodDonation.Services.Data.Appointment;
    using BloodDonation.Web.ViewModels.Appointment;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentController : Controller
    {
        private readonly IAppointmentsService appointmnetsService;
        private readonly UserManager<ApplicationUser> userMenager;

        public AppointmentController(IAppointmentsService appointmnetsService, UserManager<ApplicationUser> userMenager)
        {
            this.appointmnetsService = appointmnetsService;
            this.userMenager = userMenager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = await this.userMenager.GetUserAsync(this.User);
            var currentRecipientId = currentUser.Recipient.Id;

            await this.appointmnetsService.CreateAsync(model, currentRecipientId);
            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            const int ItemPerPage = 4;
            var viewModel = new AppointmentsListViewModel
            {
                ItemPerPage = ItemPerPage,
                PageNumber = id,
                AppointmentsCount = this.appointmnetsService.GetCount(),
                Appointments = this.appointmnetsService.GetAll(id, ItemPerPage),
            };

            return this.View(viewModel);
        }
    }
}
