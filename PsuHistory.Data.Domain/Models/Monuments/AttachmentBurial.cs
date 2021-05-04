using System;

namespace PsuHistory.Data.Domain.Models.Monuments
{
    public class AttachmentBurial : KeyGuidEntityBase
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }

        public Guid BurialId { get; set; }
        public virtual Burial Burial { get; set; }
    }
}
