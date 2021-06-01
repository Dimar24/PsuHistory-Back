using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Business.DTO.Models.AccountDataModels
{
    public class Login
    {
        public string Mail { get; set; }
        public string Password { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }
    }
}
