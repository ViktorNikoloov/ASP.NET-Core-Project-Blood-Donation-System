namespace BloodDonation.Data.Models
{
    using BloodDonation.Data.Common.Models;
    using BloodDonation.Data.Models.Enums;

    public abstract class BasicUserInfo : BaseModel<int>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public Address Address { get; set; }
    }
}
