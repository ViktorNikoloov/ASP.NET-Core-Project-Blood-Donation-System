namespace BloodDonation.Services.Data.DTO
{
    using System;

    public class GetAppointmentById
    {
        public int Id { get; set; }

        public string HospitalName { get; set; }

        public string HospitalWardName { get; set; }

        public string HospitalTownName { get; set; }

        public int BloodBankCount { get; set; }

        public string SendingAddressInfo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DeadLine { get; set; }

        public bool IsApproved { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
