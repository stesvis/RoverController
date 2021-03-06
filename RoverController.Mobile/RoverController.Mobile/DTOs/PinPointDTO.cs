﻿using Newtonsoft.Json;
using RoverController.Mobile.Misc;

namespace RoverController.Mobile.DTOs
{
    public class PinPointDTO : BaseDTO
    {
        [JsonProperty("missionId")]
        public int MissionId { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        #region Custom

        [JsonIgnore]
        public PinPointType Type { get; set; }

        [JsonIgnore]
        public bool IsVisible
        {
            get { return Type != PinPointType.Intermediate; }
        }

        #endregion Custom
    }
}