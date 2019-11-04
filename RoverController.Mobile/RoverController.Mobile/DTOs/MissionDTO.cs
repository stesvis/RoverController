using Newtonsoft.Json;
using System.Collections.Generic;

namespace RoverController.Mobile.DTOs
{
    public class MissionDTO : BaseDTO
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
        public ICollection<PinPointDTO> PinPoints { get; set; }

        private string _attachment;
        [JsonProperty("attachment")]
        public string Attachment
        {
            get { return _attachment; }
            set { SetProperty(ref _attachment, value); }
        }

        public MissionDTO()
        {
            PinPoints = new HashSet<PinPointDTO>();
        }
    }
}