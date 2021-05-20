using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace PsuHistory.Business.DTO.Models.CreateDataModels
{
    public class CreateBurial
    {
        public int NumberBurial { get; set; }
        public string Location { get; set; }
        public int KnownNumber { get; set; }
        public int UnknownNumber { get; set; }
        public int Year { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public Guid TypeBurialId { get; set; }

        public ICollection<IFormFile> Files { get; set; }
    }
}
