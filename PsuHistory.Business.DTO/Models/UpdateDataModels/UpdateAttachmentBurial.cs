using Microsoft.AspNetCore.Http;
using System;

namespace PsuHistory.Business.DTO.Models.UpdateDataModels
{
    public class UpdateAttachmentBurial : KeyGuidEntityBase
    {
        public Guid BurialId { get; set; }
        public IFormFile File { get; set; }
    }
}
