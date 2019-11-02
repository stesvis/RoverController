using Newtonsoft.Json;

namespace RoverController.Mobile.DTOs
{
    public class UserRoleDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
    }

}
