﻿namespace BloodDonation.Web.Controllers
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
            var isDonorExist = this.recipientsService.CheckRecipientExist(userId);
            var donorId = this.recipientsService.GetRecipientIdByUserId(userId);

            if (!isDonorExist)
            {
                return this.Redirect("/Home/StatusCodeError");
            }

            const int ItemPerPage = 2;
            var viewModel = new AllAppointmentsListViewModel
            {
                ItemPerPage = ItemPerPage,
                PageNumber = id,
                AppointmentsCount = this.recipientsService.GetAllAppointmentsApllyByRecipientCount(donorId),
                Appointments = this.recipientsService.GetAll(userId, id, ItemPerPage),
            };

            return this.View(viewModel);
        }
    }
}
