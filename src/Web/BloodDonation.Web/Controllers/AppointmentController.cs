namespace BloodDonation.Web.Controllers
{
    using System.Threading.Tasks;

    using BloodDonation.Services.Data.Appointment;
    using BloodDonation.Web.Infrastructure;
    using BloodDonation.Web.ViewModels.Appointment;

    using Microsoft.AspNetCore.Mvc;

    public class AppointmentController : Controller
    {
        private readonly IAppointmentsService appointmnetsService;

        public AppointmentController(IAppointmentsService appointmnetsService)
        {
            this.appointmnetsService = appointmnetsService;
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
            var recipientId = this.appointmnetsService.GetRecipientIdByUserId(this.User.GetId());

            await this.appointmnetsService.CreateAsync(model, recipientId);
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
