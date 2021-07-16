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
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.TempData["Message"] = "Вашата заявка за \"Кръводарител\" се изпрати успешно, моля изчакайте одобрение от \"Администратор\"";

            return this.Redirect("/");
        }
    }
}
