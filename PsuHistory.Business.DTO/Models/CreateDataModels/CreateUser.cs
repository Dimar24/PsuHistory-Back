using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Business.DTO.Models.CreateDataModels
{
    public class CreateUser
    {
        public string Mail { get; set; }
        public string Password { get; set; }

        public Guid RoleId { get; set; }
    }
}
