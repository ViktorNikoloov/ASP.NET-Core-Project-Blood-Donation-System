﻿namespace BloodDonation.Services.Data.DTO
{
    using System;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Mapping;

    public class GetDonorByIdDto : IMapFrom<Donor>
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public BloodType BloodType { get; set; }

        public DateTime LastDonation { get; set; }

        public string CityName { get; set; }

        public string StreetName { get; set; }

        public int? PostCode { get; set; }

        public string ImageUrl { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
