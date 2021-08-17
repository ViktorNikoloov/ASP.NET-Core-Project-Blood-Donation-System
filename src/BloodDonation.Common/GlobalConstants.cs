namespace BloodDonation.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Супер-Кръв-БГ";

        public const string MainPageName = "Начална страница";

        public const string AdministratorRoleName = "Administrator";

        public const string DonorRoleName = "Donor";

        public const string RecipientRoleName = "Recipient";

        public const string UnapprovedUserRoleName = "UnapprovedUser";

        public const string SystemEmail = "skorpions_1@abv.bg";

        public const string DonorHasNotInformationBigBubble = "Вашият профил не е попълнен !";

        public const string DonorHasNotInformationSmallBubble = "Вашият профил не е попълнен ! Моля, попълнете Своите данни, kато влезете в профила си.";

        public const string DateValidationAttributeMessage = "Началният срок трябва да е минимум от днес до след 1 месец";

        public const string EndTimeValidationAttributeMessage = "Крайният срок не трябва да бъде преди началния";

        public const int TopDonationsCount = 10;

        public const int PaginationStartPageNumber = 1;

        public const int DonationMinimumPeriod = 60; // A person could donate blood every sixty days

        public const string DonorApproveApplicantResponse = "Искаме да ви уведомим, че вашата кандидатура за кръводарител беше успешно одобрена. Вече можете да влезете в профила си. <hr> Моля не забравяйте да попълните данните си, за да можете да търсите молби за кръв. <hr> Лек и успешен ден.";

        public const string DonorRejectApplicantResponse = "Искаме да ви уведомим, че  за съжеление вашата кандидатура за кръводарител беше отхвърлена. <hr> Не се отказвайте от мечтите си да помагате на хората, пробвайте отново. <hr> Лек и успешен ден.";

        public const string RecipientApprovedAppointmentResponse = "Искаме да ви уведомим, че вашата молба за кръв беше успешно одобрена. Вече можете очаквате, някой от кръводърителите да се свърже с вас. <hr> Лек и успешен ден.";

        public const string RecipientRejectedAppointmentResponse = "Искаме да ви уведомим, че  за съжеление вашата молба за кръв беше отхвърлена. <hr> При нататъчни въпроси, моля свържете се с нас през формата за контакти в сайта ни. <hr> Лек и успешен ден.";

        public const string DefaulPicturetUrl = "https://res.cloudinary.com/dvvbab0fs/image/upload/v1627247340/faoqwxe5cyxcadm0moks.jpg";

        public const string NotFoundPictureUrl = "https://res.cloudinary.com/dvvbab0fs/image/upload/v1628995020/samples/404animation_akbyvk.gif";
    }
}
