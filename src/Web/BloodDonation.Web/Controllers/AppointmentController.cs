namespace BloodDonation.Web.Controllers
{
    using System.Text;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Services.Data.Appointment;
    using BloodDonation.Services.Data.Donor;
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
        private readonly IEmailSender emailSender;

        public AppointmentController(
            IAppointmentsService appointmnetsService,
            IDonorsService donorsService,
            IViewRenderService viewRenderService,
            IEmailSender emailSender)
        {
            this.appointmnetsService = appointmnetsService;
            this.donorsService = donorsService;
            this.viewRenderService = viewRenderService;
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
                AppointmentsCount = this.appointmnetsService.GetCount(),
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

            var html = new StringBuilder();
            html.AppendLine($"<h1>{subject}</h1>")
                .AppendLine("<hr>")
                .AppendLine("<h3>Информация за Молбата:</h3>")
                .AppendLine($"<h5>Начало на събитието: {appointmentAllInfo.StartDate}</h5>")
                .AppendLine($"<h5>Краен срок: {appointmentAllInfo.DeadLine}</h5>")
                .AppendLine($"<h5>Кръвна група: {appointmentAllInfo.BloodType}</h5>")
                .AppendLine($"<h5>Банки кръв: {appointmentAllInfo.BloodBankCount}/бр.</h5>")
                .AppendLine("<hr>")
                .AppendLine("<h3>Информация за Реципиента:</h3>")
                .AppendLine($"<h5>Имена: {appointmentAllInfo.RecipientFullName}</h5>")
                .AppendLine($"<h5>Телефонен номер: {appointmentAllInfo.PhoneNumber}</h5>")
                .AppendLine($"<h5>Имейл адрес: {appointmentAllInfo.Email}</h5>")
                .AppendLine($"<h5>Град: {appointmentAllInfo.CityName}</h5>")
                .AppendLine("<hr>")
                .AppendLine("<h3>Информация за Болницата:</h3>")
                .AppendLine($"<h5>Име на Болница: {appointmentAllInfo.HospitalName}</h5>")
                .AppendLine($"<h5>Болнично отделение: {appointmentAllInfo.HospitalWard}</h5>")
                .AppendLine($"<h5>Града на Болницата: {appointmentAllInfo.HospitalCity}</h5>")
                .AppendLine("<hr>")
                .AppendLine("<h3>Допълнителна информация:</h3>")
                .AppendLine($"<h5>Начин и адрес за получаване: {appointmentAllInfo.SendingAddressInfo}</h5>")
                .AppendLine($"<h5>Допълнителна информация: {appointmentAllInfo.AdditionalInfo}</h5>");

            await this.emailSender.SendEmailAsync(Common.GlobalConstants.SystemEmail, Common.GlobalConstants.SystemName, donorEmail, subject, html.ToString());
            this.TempData["Message"] = $"Желана молба за кръв беше успешно изпратена на имейл: \"{donorEmail}\"";

            return this.RedirectToAction(nameof(this.AppointmentById), new { id });
        }

        private string GetCurrentRecipientId()
        => this.appointmnetsService.GetRecipientIdByUserId(this.User.GetId());
    }
}
