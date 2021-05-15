using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Business.DTO.Models.UpdateDataModels
{
    public class UpdateBirthPlace : KeyGuidEntityBase
    {
        public string Place { get; set; }
    }
}
