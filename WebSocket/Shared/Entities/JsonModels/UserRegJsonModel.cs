using Newtonsoft.Json;

namespace Shared.Entities.JsonModels
{
    public class UserRegJsonModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
