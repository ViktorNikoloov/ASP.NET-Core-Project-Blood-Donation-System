namespace BloodDonation.Data.Models
{
   public class AppointmentsDonors
    {
        public int Id { get; set; }

        public string DonorId { get; set; }

        public virtual Donor Donor { get; set; }

        public int AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
