using System.Collections.Generic;

namespace BloodDonation.Services.Data.DTO
{
    public class GetDonorsWithAppointment
    {
        public IEnumerable<int> RecipientId { get; set; }

        public IEnumerable<string> DonorId { get; set; }
    }
}
