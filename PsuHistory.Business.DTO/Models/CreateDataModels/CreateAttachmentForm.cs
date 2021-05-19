using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Business.DTO.Models.CreateDataModels
{
    public class CreateAttachmentForm : KeyGuidEntityBase
    {
        public Guid FormId { get; set; }
        public IFormFile File { get; set; }
    }
}
