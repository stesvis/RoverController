using Newtonsoft.Json;
using System;

namespace RoverController.Mobile.DTOs
{
    public class PinPointDTO
    {
        [JsonProperty("missionId")]
        public int MissionId { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("createdDateFormatted")]
        public string CreatedDateFormatted { get; set; }

        [JsonProperty("createdByUserId")]
        public string CreatedByUserId { get; set; }

        [JsonProperty("createdByName")]
        public string CreatedByName { get; set; }
    }
}