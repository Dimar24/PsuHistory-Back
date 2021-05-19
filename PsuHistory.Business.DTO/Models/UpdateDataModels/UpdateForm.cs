using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PsuHistory.Business.DTO.Models.UpdateDataModels
{
    public class UpdateForm : KeyGuidEntityBase
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public ICollection<IFormFile> Files { get; set; }
    }
}
