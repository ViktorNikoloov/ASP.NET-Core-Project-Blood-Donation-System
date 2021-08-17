namespace BloodDonation.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Data.Administator;
    using BloodDonation.Services.Data.Appointment;
    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Services.Data.Email;
    using BloodDonation.Services.Data.Recipient;
    using BloodDonation.Services.Data.Settings;
    using BloodDonation.Services.Messaging;
    using BloodDonation.Web.Infrastructure;
    using BloodDonation.Web.ViewModels.Administration.Dashboard;
    using BloodDonation.Web.ViewModels.Appointment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IAdministratorService administratorService;
        private readonly IAppointmentsService appointmentsService;
        private readonly IRecipientsService recipientsService;
        private readonly IEmailSender emailSender;
        private readonly IEmailsService emailsService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            ISettingsService settingsService,
            IAdministratorService administratorService,
            IAppointmentsService appointmentsService,
            IRecipientsService recipientsService,
            IEmailSender emailSender,
            IEmailsService emailsService,
            UserManager<ApplicationUser> userManager)
        {
            this.settingsService = settingsService;
            this.administratorService = administratorService;
            this.appointmentsService = appointmentsService;
            this.recipientsService = recipientsService;
            this.emailSender = emailSender;
            this.emailsService = emailsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }

        public IActionResult RegulationPage()
        {
            return this.View();
        }

        public async Task<IActionResult> RegulateApplicants()
        {
            var applicants = await this.userManager.GetUsersInRoleAsync(GlobalConstants.UnapprovedUserRoleName);
            applicants.OrderBy(a => a.CreatedOn);

            var viewModel = new UnapprovedUsersViewModel
            {
                Applicants = applicants,
            };

            return this.View(viewModel);
        }

        public IActionResult RegulateComments()
        {
            return this.View();
        }

        public IActionResult RegulateAppointments()
        {
            return this.View();
        }

        public IActionResult ApplicantById(string id)
        {
            var applicantViewModel = this.administratorService.ApplicantDetailsById<ApplicantViewModel>(id);

            return this.View(applicantViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveApplicant(string id)
        {
            var applicant = await this.userManager.FindByIdAsync(id);

            if (applicant == null)
            {
                return this.NotFound();
            }

            await this.userManager.RemoveFromRoleAsync(applicant, GlobalConstants.UnapprovedUserRoleName);
            await this.userManager.AddToRoleAsync(applicant, GlobalConstants.DonorRoleName);
            await this.administratorService.AddDonorAsync(id);

            var subject = "Вашата кандидатура за Кръводарител беше успешно одобрена";
            var content = GlobalConstants.DonorApproveApplicantResponse;

            await this.GenerateEmail(applicant, subject, content, "Applicant");

            this.TempData["isSuccessful"] = $"Успешно одобрихте кандидат {applicant.Email}";

            return this.RedirectToAction("RegulateApplicants");
        }

        [HttpPost]
        public async Task<IActionResult> RejectApplicant(string id)
        {
            var applicant = await this.userManager.FindByIdAsync(id);

            await this.userManager.RemoveFromRoleAsync(applicant, GlobalConstants.UnapprovedUserRoleName);
            await this.administratorService.RemoveQuestionsAnswersFromUserAsync(id);

            if (applicant == null)
            {
                return this.NotFound();
            }

            var result = await this.userManager.DeleteAsync(applicant);

            if (result.Succeeded)
            {
                this.TempData["isRejected"] = $"Успешно отхвърлихте кандидат {applicant.Email}";

                return this.RedirectToAction("RegulateApplicants");
            }

            var subject = "Вашата кандидатура за Кръводарител беше отхвърлена";
            var content = GlobalConstants.DonorRejectApplicantResponse;

            await this.GenerateEmail(applicant, subject, content, "Applicant");

            return this.View("RegulateApplicants");
        }

        public IActionResult AllNotApproved(int id = GlobalConstants.PaginationStartPageNumber)
        {
            const int ItemPerPage = 4;
            var appointmentStatus = "NotApproved";

            var viewModel = new AppointmentsListViewModel
            {
                ItemPerPage = ItemPerPage,
                PageNumber = id,
                AppointmentsCount = this.appointmentsService.GetCount(false),
                Appointments = this.appointmentsService.GetAll(id, ItemPerPage, appointmentStatus),
            };

            this.TempData["ForApproval"] = appointmentStatus;

            return this.View("All", viewModel);
        }

        public IActionResult AllApproved(int id = GlobalConstants.PaginationStartPageNumber)
        {
            const int ItemPerPage = 4;
            var viewModel = new AppointmentsListViewModel
            {
                ItemPerPage = ItemPerPage,
                PageNumber = id,
                AppointmentsCount = this.appointmentsService.GetCount(false),
                Appointments = this.appointmentsService.GetAll(id, ItemPerPage, "Approved"),
            };

            return this.View("All", viewModel);
        }

        public IActionResult AppointmentById(int id)
        {
            var viewModel = this.administratorService.GetAppoinmentAllInfo(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAppoinment(int id)
        {
            await this.administratorService.ApproveAppointmentAsync(id);

            this.TempData["isApproved"] = $"Молбата беше одобрена.";

            var recipientEmail = this.appointmentsService.GetRecipientEmailByAppointmentId(id);
            var subject = "Вашата молба за кръв беше успешно одобрена.";
            var content = GlobalConstants.RecipientApprovedAppointmentResponse;

            await this.GenerateEmail(null, subject, content, "Appointment", recipientEmail);

            return this.RedirectToAction(nameof(this.AllNotApproved));
        }

        [HttpPost]
        public async Task<IActionResult> RejectAppoinment(int id)
        {
            var recipientEmail = this.appointmentsService.GetRecipientEmailByAppointmentId(id);
            var subject = "Вашата молба за кръв беше отхвърлена";
            var content = GlobalConstants.RecipientRejectedAppointmentResponse;

            await this.administratorService.RejectAppointmentAsync(id);
            await this.GenerateEmail(null, subject, content, "Appointment", recipientEmail);

            this.TempData["isRejected"] = $"Молбата беше Отхвърлена.";

            return this.RedirectToAction(nameof(this.AllNotApproved));
        }

        public IActionResult UpdateAppoinment(int id)
        {
            var appointmentViewModel = this.appointmentsService.GetCurrentAppointment(id);

            return this.View(appointmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppoinment(GetAppointmentById model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.AppointmentById), model);
            }

            await this.administratorService.UpdateAppoinmentAsync(model);

            this.TempData["IsSuccessful"] = $"Молбата беше редактирана успешно.";

            return this.RedirectToAction(nameof(this.AllApproved));
        }

        private async Task GenerateEmail(ApplicationUser? model, string subject, string content, string toWhom, string email = "")
        {
            var emailHtml = string.Empty;
            var curremail = string.Empty;
            if (toWhom == "Applicant")
            {
                emailHtml = this.emailsService.GenerateEmailApplicantResponseHtmlContent(model, subject, content);
            }
            else if (toWhom == "Appointment")
            {
                emailHtml = this.emailsService.GenerateEmailAppoinmentResponseHtmlContent(model, subject, content);
            }

            if (email != string.Empty)
            {
                curremail = email;
            }
            else
            {
                curremail = model.Email;

            }

            await this.emailSender.SendEmailAsync(GlobalConstants.SystemEmail, GlobalConstants.SystemName, curremail, subject, emailHtml);
        }

        private string GetRecipientEmail()
        => this.recipientsService.GetRecipientEmail(this.User.GetId());

    }
}
