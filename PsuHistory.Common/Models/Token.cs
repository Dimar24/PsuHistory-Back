using Newtonsoft.Json;

namespace PsuHistory.Common.Models
{
    public class Token
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
    }
}
