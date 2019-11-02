using Newtonsoft.Json;

namespace RoverController.Mobile.DTOs
{
    public class ErrorMessageResponseDTO
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("exceptionMessage")]
        public string ExceptionMessage { get; set; }
    }
}