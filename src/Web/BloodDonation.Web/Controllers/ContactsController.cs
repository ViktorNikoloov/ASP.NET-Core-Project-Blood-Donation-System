namespace BloodDonation.Web.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Messaging;
    using BloodDonation.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : BaseController
    {
        private const string RedirectedFromContactForm = "RedirectedFromContactForm";

        private readonly IRepository<ContactFormEntry> contactsRepository;

        private readonly IEmailSender emailSender;

        public ContactsController(IRepository<ContactFormEntry> contactsRepository, IEmailSender emailSender)
        {
            this.contactsRepository = contactsRepository;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // TODO: Extract to IP provider (service)
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
            var contactFormEntry = new ContactFormEntry
            {
                Name = model.Name,
                Email = model.Email,
                Title = model.Title,
                Content = model.Content,
                Ip = ip,
            };
            await this.contactsRepository.AddAsync(contactFormEntry);
            await this.contactsRepository.SaveChangesAsync();

            var htmlEmailContent = new StringBuilder();
            htmlEmailContent
                .AppendLine($"<h1>Sender's email: <u>{model.Email}</u></h1>")
                .AppendLine($"{model.Content}>");

            await this.emailSender.SendEmailAsync(
                GlobalConstants.SystemEmail, // Need to put the email which is Sendgrid Verified !
                model.Name,
                GlobalConstants.SystemEmail, // Put the email on which you want to receive the messages.
                model.Title,
                htmlEmailContent.ToString().TrimEnd());

            this.TempData[RedirectedFromContactForm] = true;

            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            if (this.TempData[RedirectedFromContactForm] == null)
            {
                return this.NotFound();
            }

            return this.View();
        }
    }
}
