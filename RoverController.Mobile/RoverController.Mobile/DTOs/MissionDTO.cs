using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoverController.Mobile.DTOs
{
    public class MissionDTO
    {
        [JsonProperty("maxX")]
        public int MaxX { get; set; }

        [JsonProperty("maxY")]
        public int MaxY { get; set; }

        [JsonProperty("initialX")]
        public int InitialX { get; set; }

        [JsonProperty("initialY")]
        public int InitialY { get; set; }

        [JsonProperty("initialDirection")]
        public string InitialDirection { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        [JsonProperty("finalX")]
        public int FinalX { get; set; }

        [JsonProperty("finalY")]
        public int FinalY { get; set; }

        [JsonProperty("finalDirection")]
        public string FinalDirection { get; set; }

        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("output")]
        public string Output { get; set; }

        [JsonProperty("pinPoints")]
        public List<PinPointDTO> PinPoints { get; set; }

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