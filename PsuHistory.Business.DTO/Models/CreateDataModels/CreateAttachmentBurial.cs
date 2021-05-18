using Microsoft.AspNetCore.Http;
using System;

namespace PsuHistory.Business.DTO.Models.CreateDataModels
{
    public class CreateAttachmentBurial : KeyGuidEntityBase 
    {
        public IFormFile File { get; set; }
        public Guid FormId { get; set; }
    }
}
