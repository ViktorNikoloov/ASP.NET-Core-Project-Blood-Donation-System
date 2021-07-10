namespace BloodDonation.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonation.Data.Common.Models;

    [NotMapped]
    public class Massage : BaseDeletableModel<int>
    {
        public string DonorId { get; set; }

        public virtual Donor Donor { get; set; }

        public string RecipientId { get; set; }

        public virtual Recipient Recipient { get; set; }

        public IDictionary<string, IEnumerable<string>> Massages { get; set; }
    }
}
