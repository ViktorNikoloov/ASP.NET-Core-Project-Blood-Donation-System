namespace BloodDonation.Web.Controllers
{
    using BloodDonation.Web.ViewModels.Donor;

    using Microsoft.AspNetCore.Mvc;

    public class DonorController : Controller
    {
        public IActionResult BeADonor()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult BeADonor(DonorFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            return RedirectToAction("Settings");
        }
    }
}
