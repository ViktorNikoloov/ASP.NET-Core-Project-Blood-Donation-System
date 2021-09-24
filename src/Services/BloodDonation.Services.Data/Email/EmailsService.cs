namespace BloodDonation.Services.Data.Email
{
    using System.Text;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Web.ViewModels.Appointment;

    public class EmailsService : IEmailsService
    {
        public string GenerateEmailApplicantResponseHtmlContent(ApplicationUser? model, string subject, string content, string email = "")
        {
            var htmlContent = new StringBuilder();

            htmlContent.AppendLine($"<h1>{subject}</h1>")
                .AppendLine("<hr>")
                .AppendLine("<h3>Информация за Кандидатурата:</h3>")
                .AppendLine($"<h5>{content}</h5>");

            return htmlContent.ToString();
        }

        public string GenerateEmailAppoinmentResponseHtmlContent(ApplicationUser? model, string subject, string content, string email = "")
        {
            var htmlContent = new StringBuilder();

            htmlContent.AppendLine($"<h1>{subject}</h1>")
                .AppendLine("<hr>")
                .AppendLine("<h3>Информация за Молбата за кръв:</h3>")
                .AppendLine($"<h5>{content}</h5>");

            return htmlContent.ToString();
        }

        public string GenerateEmailAppointmentHtmlContent(AppointmentByIdViewModel appointmentAllInfo, string subject)
        {
            var htmlContent = new StringBuilder();

            htmlContent.AppendLine($"<h1>{subject}</h1>")
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

            return htmlContent.ToString();
        }

        public string GenerateEmailDonorSendApplication(ApplicationUser user, string subject)
        {
            var htmlContent = new StringBuilder();
            htmlContent.AppendLine($"<h1>{subject}</h1>")
                .AppendLine("<hr>")
                .AppendLine($"<h3>Информация за кръводарителя:</h3>")
                .AppendLine($"<h5>Имейл адрес: {user.Email}</h5>")
                .AppendLine($"<h5>Телефонен номер: {user.PhoneNumber}</h5>")
                .AppendLine("<hr>")
                .AppendLine($"<h3>Въпроси и отговори:</h5>");
            foreach (var questionAnswer in user.QuestionsAnswers)
            {
                var translateAnswer = questionAnswer.Answer == "True" ? "Да" : "Не";
                htmlContent.AppendLine($"<h5>-{questionAnswer.Question} - {translateAnswer}</h5>");
            }

            return htmlContent.ToString();
        }

        public string GenerateEmailDonorTakeAnAppointmentHtmlContent(GetDonorByIdDto model, string subject)
        {
            var htmlContent = new StringBuilder();

            htmlContent.AppendLine($"<h1>{subject}</h1>")
                .AppendLine("<hr>")
                .AppendLine("<h3>Информация за Кръводарителя:</h3>")
                .AppendLine($"<h5>Имена: {model.FirstName} {model.LastName}</h5>")
                .AppendLine($"<h5>Телефонен номер: {model.PhoneNumber}</h5>")
                .AppendLine($"<h5>Имейл адрес: {model.Email}</h5>")
                .AppendLine($"<h5>Град: {model.CityName}</h5>");

            return htmlContent.ToString();
        }

        public string GenerateEmailRecipientNewRegistration(ApplicationUser user, string subject)
        {
            var htmlContent = new StringBuilder();
            var genderInBulgarian = user.Recipient.Gender == Gender.Male ? "Мъж" : "Жена";
            var enumDisplayName = this.EnumHelperDisplayName(user.Recipient.BloodType);

            htmlContent.AppendLine($"<h1>{subject}</h1>")
                .AppendLine("<hr>")
                .AppendLine($"<h3>Информация за реципиента:</h3>")
                .AppendLine($"<h5>Имена: {user.Recipient.FirstName} {user.Recipient.MiddleName} {user.Recipient.LastName}</h5>")
                .AppendLine($"<h5>Кръвна група: {enumDisplayName}</h5>")
                .AppendLine($"<h5>Пол: {genderInBulgarian}</h5>")
                .AppendLine($"<h5>Град: {user.Recipient.Address.Town.Name}</h5>")
                .AppendLine($"<h5>Улица: {user.Recipient.Address.Town.Street.Name}</h5>")
                .AppendLine($"<h5>Телефонен номер: {user.PhoneNumber}</h5>")
                .AppendLine($"<h5>Имейл адрес: {user.Email}</h5>");

            return htmlContent.ToString();
        }

        private string EnumHelperDisplayName(BloodType bloodType)
        {
            string enumDisplayName = string.Empty;

            if (bloodType == BloodType.Unknown)
            {
                enumDisplayName = "Липсва";
            }
            else if (bloodType == BloodType.APositive)
            {
                enumDisplayName = "A(+)";
            }
            else if (bloodType == BloodType.ANegative)
            {
                enumDisplayName = "A(-)";
            }
            else if (bloodType == BloodType.BPositive)
            {
                enumDisplayName = "B(+)";
            }
            else if (bloodType == BloodType.BNegative)
            {
                enumDisplayName = "B(-)";
            }
            else if (bloodType == BloodType.ABPositive)
            {
                enumDisplayName = "AB(+)";
            }
            else if (bloodType == BloodType.ABNegative)
            {
                enumDisplayName = "AB(-)";
            }
            else if (bloodType == BloodType.ZeroPositive)
            {
                enumDisplayName = "0(+)";
            }
            else if (bloodType == BloodType.ZeroNegative)
            {
                enumDisplayName = "0(-)";
            }

            return enumDisplayName;
        }
    }
}
