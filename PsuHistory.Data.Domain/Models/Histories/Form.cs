using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsuHistory.Data.Domain.Models.Histories
{
    public class Form : KeyGuidEntityBase
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public virtual ICollection<AttachmentForm> AttachmentForms { get; set; } = new List<AttachmentForm>();

        [NotMapped]
        public virtual ICollection<IFormFile> Files { get; set; }
    }
}
