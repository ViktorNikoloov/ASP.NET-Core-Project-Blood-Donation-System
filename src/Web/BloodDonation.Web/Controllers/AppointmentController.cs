namespace BloodDonation.Web.Controllers
{
    using System.Text;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Services.Data.Appointment;
    using BloodDonation.Services.Data.Donor;
    using BloodDonation.Services.Data.Email;
    using BloodDonation.Services.Data.ViewRenderService;
    using BloodDonation.Services.Messaging;
    using BloodDonation.Web.Infrastructure;
    using BloodDonation.Web.ViewModels.Appointment;

    using Microsoft.AspNetCore.Mvc;

    public class AppointmentController : Controller
    {
        private readonly IAppointmentsService appointmnetsService;
        private readonly IDonorsService donorsService;
        private readonly IViewRenderService viewRenderService;
        private readonly IEmailsService emailsService;
        private readonly IEmailSender emailSender;

        public AppointmentController(
            IAppointmentsService appointmnetsService,
            IDonorsService donorsService,
            IViewRenderService viewRenderService,
            IEmailSender emailSender,
            IEmailsService emailsService
            )
        {
            this.appointmnetsService = appointmnetsService;
            this.donorsService = donorsService;
            this.viewRenderService = viewRenderService;
            this.emailsService = emailsService;
            this.emailSender = emailSender;
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

            var recipientId = this.GetCurrentRecipientId();

            await this.appointmnetsService.CreateAsync(model, recipientId);
            return this.Redirect("/");
        }

        public IActionResult All(int id = GlobalConstants.PaginationStartPageNumber)
        {
            const int ItemPerPage = 4;
            var viewModel = new AppointmentsListViewModel
            {
                ItemPerPage = ItemPerPage,
                PageNumber = id,
                AppointmentsCount = this.appointmnetsService.GetCount(true),
                Appointments = this.appointmnetsService.GetAll(id, ItemPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult AppointmentById(int id)
        {
            var viewModel = this.appointmnetsService.GetAppoinmentAllInfo(id);
            var isDonorExistInDonorsAppointmetns = this.appointmnetsService.IsDonorExistInDonorsAppointmetns(id, this.User.GetId());

            if (isDonorExistInDonorsAppointmetns)
            {
                this.TempData["NotFoundMessage"] = $"Вече сте се отзовали на тази молба.";
                return this.RedirectToAction(nameof(this.All));
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> TakeAppointmentByDonor(int id)
        {
            await this.appointmnetsService.TakeAppointmentByDonor(this.User.GetId(), id);

            return this.Redirect("/");
        }

        public async Task<IActionResult> SendToEmail(int id)
        {
            // var email = this.viewRenderService.RenderToStringAsync(); //string viewName, object model
            var appointmentAllInfo = this.appointmnetsService.GetAppoinmentAllInfo(id);
            var donorEmail = this.donorsService.GetDonorEmailByUserId(this.User.GetId());

            var subject = $"Молба за кръв на {appointmentAllInfo.RecipientFullName}";
            var emailHtml = this.emailsService.GenerateEmailAppointmentHtmlContent(appointmentAllInfo, subject);

            await this.emailSender.SendEmailAsync(Common.GlobalConstants.SystemEmail, Common.GlobalConstants.SystemName, donorEmail, subject, emailHtml);

            this.TempData["Message"] = $"Желана молба за кръв беше успешно изпратена на имейл: \"{donorEmail}\"";

            return this.RedirectToAction(nameof(this.AppointmentById), new { id });
        }

        private string GetCurrentRecipientId()
        => this.appointmnetsService.GetRecipientIdByUserId(this.User.GetId());
    }
}
