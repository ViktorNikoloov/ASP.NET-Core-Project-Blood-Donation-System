namespace BloodDonation.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Data.Appointment;
    using BloodDonation.Services.Data.Donor;
    using BloodDonation.Services.Data.Email;
    using BloodDonation.Services.Data.Recipient;
    using BloodDonation.Services.Data.ViewRenderService;
    using BloodDonation.Services.Messaging;
    using BloodDonation.Web.Infrastructure;
    using BloodDonation.Web.ViewModels.Appointment;

    using Microsoft.AspNetCore.Mvc;

    public class AppointmentController : Controller
    {
        private readonly IAppointmentsService appointmnetsService;
        private readonly IDonorsService donorsService;
        private readonly IRecipientsService recipientsService;
        private readonly IViewRenderService viewRenderService;
        private readonly IEmailsService emailsService;
        private readonly IEmailSender emailSender;

        public AppointmentController(
            IAppointmentsService appointmnetsService,
            IDonorsService donorsService,
            IRecipientsService recipientsService,
            IViewRenderService viewRenderService,
            IEmailSender emailSender,
            IEmailsService emailsService
            )
        {
            this.appointmnetsService = appointmnetsService;
            this.donorsService = donorsService;
            this.recipientsService = recipientsService;
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

            this.TempData["Message"] = $"Вашата молба беше изпратена успешно. Ще получите имейл след разглеждането й от \"Администратор\"";

            return this.Redirect("/");
        }

        public IActionResult All(int id = GlobalConstants.PaginationStartPageNumber)
        {
            var donorLastDonation = this.donorsService.GetLastTimeDonorDonaton(this.CurrUserId());
            var donorNextDonation = this.donorsService.GetWhenDonorCouldDonateAgain(donorLastDonation);
            var daysRemaining = this.donorsService.GetDonorRemainingDaysToDonation(donorLastDonation);
            if (donorNextDonation >= DateTime.UtcNow)
            {
                this.TempData["NotFoundMessage"] = $"Кръводарявал сте на \"{donorLastDonation.ToLocalTime().ToShortDateString()}\". Ще можете да кръводарявате отново след \"{daysRemaining}\" дни.";

                return this.Redirect("/");
            }

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
            return this.View(viewModel);
        }

        public async Task<IActionResult> TakeAppointmentByDonor(int id)
        {
            var isDonorExistInDonorsAppointmetns = this.appointmnetsService.IsDonorExistInDonorsAppointmetns(id, this.CurrUserId());
            if (isDonorExistInDonorsAppointmetns)
            {
                this.TempData["NotFoundMessage"] = $"Вече сте се отзовали на тази обява.";

                return this.Redirect("/Donor/All");
            }

            await this.appointmnetsService.TakeAppointmentByDonor(this.CurrUserId(), id);

            var currDonor = this.donorsService.GetDonorById(this.CurrUserId());
            var currRecipientEmail = this.appointmnetsService.GetRecipientEmailByAppointmentId(id);

            var subject = $"Вашата молба за кръв беше взета";
            var emailHtml = this.emailsService.GenerateEmailDonorTakeAnAppointmentHtmlContent(currDonor, subject);
            await this.emailSender.SendEmailAsync(GlobalConstants.SystemEmail, GlobalConstants.SystemName, currRecipientEmail, subject, emailHtml);

            return this.Redirect("/");
        }

        public async Task<IActionResult> SendToEmail(int id)
        {
            // var email = this.viewRenderService.RenderToStringAsync(); //string viewName, object model
            var appointmentAllInfo = this.appointmnetsService.GetAppoinmentAllInfo(id);
            var donorEmail = this.donorsService.GetDonorEmailByUserId(this.CurrUserId());

            var subject = $"Молба за кръв на {appointmentAllInfo.RecipientFullName}";
            var emailHtml = this.emailsService.GenerateEmailAppointmentHtmlContent(appointmentAllInfo, subject);

            await this.emailSender.SendEmailAsync(Common.GlobalConstants.SystemEmail, Common.GlobalConstants.SystemName, donorEmail, subject, emailHtml);

            this.TempData["Message"] = $"Желана молба за кръв беше успешно изпратена на имейл: \"{donorEmail}\"";

            return this.RedirectToAction(nameof(this.AppointmentById), new { id });
        }

        private async Task GenerateEmail(ApplicationUser? model, string subject, string content, string email = "")
        {
            var emailHtml = this.emailsService.GenerateEmailApplicantResponseHtmlContent(model, subject, content);

            await this.emailSender.SendEmailAsync(GlobalConstants.SystemEmail, GlobalConstants.SystemName, model.Email, subject, emailHtml);
        }

        private string GetCurrentRecipientId()
        => this.appointmnetsService.GetRecipientIdByUserId(this.CurrUserId());

        private string CurrUserId()
            => this.User.GetId();
    }
}
