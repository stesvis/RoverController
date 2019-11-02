using Newtonsoft.Json;

namespace RoverController.Mobile.DTOs
{
    public class ErrorResponseDTO
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("exceptionMessage")]
        public string ExceptionMessage { get; set; }
    }
}