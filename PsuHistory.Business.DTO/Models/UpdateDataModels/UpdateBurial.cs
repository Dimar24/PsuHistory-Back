using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Business.DTO.Models.UpdateDataModels
{
    public class UpdateBurial : KeyGuidEntityBase
    {
        public int NumberBurial { get; set; }
        public string Location { get; set; }
        public int NumberPeople { get; set; }
        public int UnknownNumber { get; set; }
        public int Year { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public Guid TypeBurialId { get; set; }

        public ICollection<IFormFile> Files { get; set; }
    }
}
