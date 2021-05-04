using System;

namespace PsuHistory.Data.Domain.Models.Histories
{
    public class AttachmentForm : KeyGuidEntityBase
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }

        public Guid FormId { get; set; }
        public virtual Form Form { get; set; }
    }
}
