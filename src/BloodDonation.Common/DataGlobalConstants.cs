namespace BloodDonation.Common
{
    public static class DataGlobalConstants
    {
        public static class AddressConstants
        {
            public const int CountryMaxLength = 30;

            public const int CityMaxLength = 20;

            public const int StreetMaxLength = 50;

            public const int PostCodeMaxLength = 9;
        }

        public static class BasicUserInfoConstants
        {
            public const int FirstNameMaxLength = 15;

            public const int MiddleNameMaxLength = 10;

            public const int LastNameMaxLength = 20;

            public const string PhoneNumberRegex = @"^(\+)?(359|0)8[6789]\d{1}(|-| |\/)\d{3}(|-| )\d{3}$";
        }

        public static class AppointmentConstants
        {
            public const int BloodBankCountMaxLength = 15;
        }

        public static class HospitalConstants
        {
            public const int HospitalNameMaxLength = 50;

            public const int HospitalWardNameMaxLength = 80;

            public const int BloodBankCountMaxLength = 15;
        }
    }
}
