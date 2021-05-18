using Microsoft.AspNetCore.Http;
using System;

namespace PsuHistory.Business.DTO.Models.UpdateDataModels
{
    public class UpdateAttachmentBurial : KeyGuidEntityBase
    {
        public IFormFile File { get; set; }
        public Guid FormId { get; set; }
    }
}
