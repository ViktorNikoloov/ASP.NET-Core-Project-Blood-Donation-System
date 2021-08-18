namespace BloodDonation.Web.Controllers
{
    using BloodDonation.Common;
    using BloodDonation.Services.Data.Recipient;
    using BloodDonation.Web.Infrastructure;
    using BloodDonation.Web.ViewModels.Recipient;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.RecipientRoleName)]
    public class RecipientController : Controller
    {
        private readonly IRecipientsService recipientsService;

        public RecipientController(IRecipientsService recipientsService)
        {
            this.recipientsService = recipientsService;
        }

        public IActionResult All(int id = GlobalConstants.PaginationStartPageNumber)
        {
            var userId = this.User.GetId();
            var isRecipientExist = this.recipientsService.CheckRecipientExist(userId);
            var RecipientId = this.recipientsService.GetRecipientIdByUserId(userId);

            if (!isRecipientExist)
            {
                return this.Redirect("/Home/StatusCodeError");
            }

            const int ItemPerPage = 2;
            var viewModel = new AllAppointmentsListViewModel
            {
                ItemPerPage = ItemPerPage,
                PageNumber = id,
                AppointmentsCount = this.recipientsService.GetAllAppointmentsApllyByRecipientCount(RecipientId),
                Appointments = this.recipientsService.GetAll(userId, id, ItemPerPage),
            };

            return this.View(viewModel);
        }
    }
}
