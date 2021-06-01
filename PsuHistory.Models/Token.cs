using Newtonsoft.Json;

namespace PsuHistory.Models
{
    public class Token
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
    }
}
