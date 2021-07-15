namespace BloodDonation.Common
{
    public static class DataGlobalConstants
    {
        public static class AddressConstants
        {
            public const int CountryMaxLength = 30;

            public const int CityMinLength = 3;

            public const int CityMaxLength = 20;

            public const int StreetMaxLength = 50;

            public const int PostCodeMinValue = 1;

            public const int PostCodeMaxValue = 9999;
        }

        public static class BasicUserInfoConstants
        {
            public const int FirstNameMinLength = 3;

            public const int FirstNameMaxLength = 15;

            public const int MiddleNameMinLength = 5;

            public const int MiddleNameMaxLength = 15;

            public const int LastNameMinLength = 5;

            public const int LastNameMaxLength = 20;

            public const string PhoneNumberRegex = @"^(\+)?(359|0)8[6789]\d{1}(|-| |\/)\d{3}(|-| )\d{3}$";
        }

        public static class AppointmentConstants
        {
            public const int BloodBankCountMaxLength = 15;
        }

        public static class StreetConstants
        {
            public const int StreetNameMinLength = 3;

            public const int StreetNameMaxLength = 50;

            public const int StreetNumberMinValue = 0;

            public const int StreetNumberMaxValue = 999;
        }

        public static class HospitalConstants
        {
            public const int HospitalNameMaxLength = 50;

            public const int HospitalWardNameMaxLength = 80;

            public const int BloodBankCountMaxLength = 15;
        }
    }
}
