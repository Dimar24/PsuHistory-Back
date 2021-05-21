using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PsuHistory.Data.Domain.Models.Histories
{
    public class AttachmentForm : KeyGuidEntityBase
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }

        public Guid FormId { get; set; }
        //[JsonIgnore]
        public virtual Form Form { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile File { get; set; }
    }
}
