using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsuHistory.Data.Domain.Models.Monuments
{
    public class AttachmentBurial : KeyGuidEntityBase
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }

        public Guid BurialId { get; set; }
        public virtual Burial Burial { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
